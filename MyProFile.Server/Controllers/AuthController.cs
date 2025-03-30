using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyProFile.Data;
using MyProFile.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyProFile.Server.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly MyProFileDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(MyProFileDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Token))
                return BadRequest("Липсва токен за покана.");

            var invitation = await _context.Invitations
                .FirstOrDefaultAsync(i => i.Token == dto.Token && !i.IsUsed && i.Expiration > DateTime.UtcNow);

            if (invitation == null)
                return BadRequest("Невалиден или изтекъл токен за покана.");

            if (!string.Equals(invitation.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
                return BadRequest("Имейлът не съвпада с този в поканата.");

            var userExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (userExists)
                return BadRequest("Потребител с този имейл вече съществува.");

            var user = new User
            {
                Email = dto.Email,
                Username = dto.Username, // Ако имаш поле Username
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = invitation.Role
            };

            _context.Users.Add(user);

            // маркираме поканата като използвана
            invitation.IsUsed = true;

            await _context.SaveChangesAsync();

            return Ok("Успешна регистрация.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Невалиден имейл или парола.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Ok(new { token = jwt });
        }
    }
}
