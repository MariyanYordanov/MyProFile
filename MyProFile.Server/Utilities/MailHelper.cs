using System.Net;
using System.Net.Mail;

namespace MyProFile.Server.Utilities
{
    public static class MailHelper
    {
        public static async Task SendInvitationEmail(string email, string token, string role)
        {
            var registrationLink = $"https://localhost:49647/register?token={token}";

            var fromAddress = new MailAddress("noreply@myprofile.bg", "MyProFile");
            var toAddress = new MailAddress(email);
            const string subject = "Покана за регистрация в MyProFile";
            string body = $@"
                <p>Получихте покана да се регистрирате в системата MyProFile като <b>{role}</b>.</p>
                <p>Моля, използвайте следния линк за да завършите регистрацията:</p>
                <p><a href=""{registrationLink}"">{registrationLink}</a></p>
                <p>Поздрави, екипът на MyProFile.</p>";

            var smtp = new SmtpClient
            {
                Host = "smtp.mailtrap.io", // TODO замени с реален SMTP
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("your_username", "your_password")
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            await smtp.SendMailAsync(message);
        }
    }
}
