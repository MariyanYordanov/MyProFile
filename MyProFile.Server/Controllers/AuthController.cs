using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MyProFile.Data;
using MyProFile.Server.DTOs;
using System.Web;

namespace MyProFile.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public AuthController(UserManager<User> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = new User
            {
                UserName = dto.Username,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);

            var confirmUrl = $"https://localhost:5173/confirm-email?userId={user.Id}&token={encodedToken}";
            await _emailSender.SendEmailAsync(user.Email, "Потвърждение на регистрация",
                $"Моля, потвърдете имейла си като последвате този линк: <a href='{confirmUrl}'>Потвърди</a>");

            return Ok("Имейл за потвърждение е изпратен.");
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest("Невалиден потребител.");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
                return BadRequest("Грешка при потвърждаване на имейла.");

            return Ok("Имейлът е потвърден успешно.");
        }
    }
}
