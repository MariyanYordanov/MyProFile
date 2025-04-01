using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProFile.Data.Models;
using MyProFile.Server.DTOs;
using MyProFile.Server.Utilities;

namespace MyProFile.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, JwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return Unauthorized("Невалидни данни за вход");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "guest";

            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email, role);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Тук можеш да съхраняваш refreshToken в базата, ако искаш

            return Ok(new
            {
                token,
                refreshToken
            });
        }
    }
}