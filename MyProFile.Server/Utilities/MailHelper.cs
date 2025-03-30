using System.Net;
using System.Net.Mail;

namespace MyProFile.Server.Utilities
{
    public class MailHelper
    {
        private readonly IConfiguration _config;
        private readonly ILogger<MailHelper> _logger;

        public MailHelper(IConfiguration config, ILogger<MailHelper> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendInvitationEmail(string email, string token, string role)
        {
            try
            {
                var smtpHost = _config["Mail:Host"];
                var smtpPortString = _config["Mail:Port"];
                var smtpUser = _config["Mail:User"];
                var smtpPass = _config["Mail:Pass"];
                var fromEmail = _config["Mail:FromEmail"];
                var fromName = _config["Mail:FromName"];
                var frontendUrl = _config["AppSettings:FrontendUrl"];

                if (string.IsNullOrWhiteSpace(smtpHost) || string.IsNullOrWhiteSpace(fromEmail) || string.IsNullOrWhiteSpace(frontendUrl))
                {
                    _logger.LogError("SMTP или Frontend конфигурацията е непълна. Проверете appsettings.json.");
                    throw new InvalidOperationException("SMTP или Frontend конфигурацията е непълна.");
                }

                if (!int.TryParse(smtpPortString, out var smtpPort))
                {
                    _logger.LogError("SMTP портът не може да се конвертира: {Port}", smtpPortString);
                    throw new InvalidOperationException("SMTP портът е невалиден.");
                }

                var registrationLink = $"{frontendUrl}/register?token={token}";

                var fromAddress = new MailAddress(fromEmail, fromName ?? "MyProFile");
                var toAddress = new MailAddress(email);
                const string subject = "Покана за регистрация в MyProFile";

                string body = $@"
                    <html>
                    <body>
                        <p>Получихте покана да се регистрирате в системата MyProFile като <strong>{role}</strong>.</p>
                        <p>Моля, използвайте следния линк за да завършите регистрацията:</p>
                        <p><a href='{registrationLink}'>{registrationLink}</a></p>
                        <p>Поздрави,<br/>екипът на MyProFile.</p>
                    </body>
                    </html>";

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
                    Body = body,
                    IsBodyHtml = true
                };

                await smtp.SendMailAsync(message);
                _logger.LogInformation("\ud83d\udcec Поканата е успешно изпратена до: {Email}", email);
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
    }
}