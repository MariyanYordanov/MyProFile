using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyProFile.Server.Utilities
{
    public class MailHelper : IEmailSender
    {
        private readonly IConfiguration _config;
        private readonly ILogger<MailHelper> _logger;

        public MailHelper(IConfiguration config, ILogger<MailHelper> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var smtpHost = _config["Mail:Host"];
                var smtpPortString = _config["Mail:Port"];
                var smtpUser = _config["Mail:User"];
                var smtpPass = _config["Mail:Pass"];
                var fromEmail = _config["Mail:FromEmail"];
                var fromName = _config["Mail:FromName"];

                if (!int.TryParse(smtpPortString, out var smtpPort))
                    throw new InvalidOperationException("SMTP портът е невалиден.");

                var fromAddress = new MailAddress(fromEmail, fromName ?? "MyProFile");
                var toAddress = new MailAddress(email);

                var smtp = new SmtpClient
                {
                    Host = smtpHost,
                    Port = smtpPort,
                    EnableSsl = false,
                    Credentials = string.IsNullOrWhiteSpace(smtpUser)
                        ? CredentialCache.DefaultNetworkCredentials
                        : new NetworkCredential(smtpUser, smtpPass)
                };

                using var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };

                await smtp.SendMailAsync(message);
                _logger.LogInformation("Имейл изпратен успешно до {Email}", email);
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError(smtpEx, "SMTP грешка при изпращане на имейл до: {Email}", email);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Обща грешка при изпращане на имейл до: {Email}", email);
                throw;
            }
        }

        public async Task SendInvitationEmail(string email, string token, string role)
        {
            var frontendUrl = _config["AppSettings:FrontendUrl"];
            if (string.IsNullOrWhiteSpace(frontendUrl))
                throw new InvalidOperationException("AppSettings:FrontendUrl липсва.");

            var registrationLink = $"{frontendUrl}/register?token={token}";

            var htmlMessage = $@"
                <html>
                <body>
                    <p>Получихте покана да се регистрирате в системата MyProFile като <strong>{role}</strong>.</p>
                    <p>Моля, използвайте следния линк за да завършите регистрацията:</p>
                    <p><a href='{registrationLink}'>{registrationLink}</a></p>
                    <p>Поздрави,<br/>екипът на MyProFile.</p>
                </body>
                </html>";

            await SendEmailAsync(email, "Покана за регистрация в MyProFile", htmlMessage);
        }
    }
}
