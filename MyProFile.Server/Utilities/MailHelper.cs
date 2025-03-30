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
                // Зареждане на конфигурация
                var smtpHost = _config["Mail:Host"];
                var smtpPortString = _config["Mail:Port"];
                var smtpUser = _config["Mail:User"];
                var smtpPass = _config["Mail:Pass"];
                var fromEmail = _config["Mail:FromEmail"];
                var fromName = _config["Mail:FromName"];

                if (string.IsNullOrWhiteSpace(smtpHost) || string.IsNullOrWhiteSpace(fromEmail))
                {
                    _logger.LogError("SMTP конфигурацията е непълна. Проверете appsettings.json.");
                    throw new InvalidOperationException("SMTP конфигурацията е непълна.");
                }

                if (!int.TryParse(smtpPortString, out var smtpPort))
                {
                    _logger.LogError("SMTP портът не може да се конвертира: {Port}", smtpPortString);
                    throw new InvalidOperationException("SMTP портът е невалиден.");
                }

                // Изграждане на съдържание
                var registrationLink = $"https://localhost:49647/register?token={token}";

                var fromAddress = new MailAddress(fromEmail, fromName ?? "MyProFile");
                var toAddress = new MailAddress(email);
                const string subject = "Покана за регистрация в MyProFile";

                string body = $@"
                    <p>Получихте покана да се регистрирате в системата MyProFile като <b>{role}</b>.</p>
                    <p>Моля, използвайте следния линк за да завършите регистрацията:</p>
                    <p><a href=""{registrationLink}"">{registrationLink}</a></p>
                    <p>Поздрави,<br/>екипът на MyProFile.</p>";

                var smtp = new SmtpClient
                {
                    Host = smtpHost,
                    Port = smtpPort,
                    EnableSsl = false, // само за smtp4dev, после true
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
                _logger.LogInformation("📬 Поканата е успешно изпратена до: {Email}", email);
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
