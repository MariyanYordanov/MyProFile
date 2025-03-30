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
                var smtpPort = int.Parse(_config["Mail:Port"]);
                var smtpUser = _config["Mail:User"];
                var smtpPass = _config["Mail:Pass"];
                var fromEmail = _config["Mail:FromEmail"];
                var fromName = _config["Mail:FromName"];

                var registrationLink = $"https://localhost:49647/register?token={token}";

                var fromAddress = new MailAddress(fromEmail, fromName);
                var toAddress = new MailAddress(email);
                const string subject = "Покана за регистрация в MyProFile";

                string body = $@"
                    <p>Получихте покана да се регистрирате в системата MyProFile като <b>{role}</b>.</p>
                    <p>Моля, използвайте следния линк за да завършите регистрацията:</p>
                    <p><a href=""{registrationLink}"">{registrationLink}</a></p>
                    <p>Поздрави, екипът на MyProFile.</p>";

                var smtp = new SmtpClient
                {
                    Host = smtpHost,
                    Port = smtpPort,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(smtpUser, smtpPass)
                };

                using var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                await smtp.SendMailAsync(message);
                _logger.LogInformation("Успешно изпратена покана до: {Email}", email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Грешка при изпращане на покана до: {Email}", email);
                throw; // ако искаме да се върне грешка на клиента
            }
        }
    }
}
