using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyProFile.Data;
using MyProFile.Data.Models;
using MyProFile.Server.DTOs;
using MyProFile.Server.Utilities;
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
        private readonly RefreshTokenHelper _refreshHelper;

        public AuthController(MyProFileDbContext context, IConfiguration configuration, RefreshTokenHelper refreshHelper)
        {
            _context = context;
            _configuration = configuration;
            _refreshHelper = refreshHelper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            var invitation = await _context.Invitations
                .FirstOrDefaultAsync(i => i.Token == dto.Token && !i.IsUsed && i.Expiration > DateTime.UtcNow);
            if (invitation == null || !string.Equals(invitation.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
                return BadRequest("Невалиден или изтекъл токен/имейл не съвпада.");

            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Потребител с този имейл вече съществува.");

            var user = new User
            {
                Email = dto.Email,
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = invitation.Role
            };

            _context.Users.Add(user);
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

            var (accessToken, refreshToken, refreshExpiry) = _refreshHelper.GenerateTokenPair(user);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = refreshExpiry;
            await _context.SaveChangesAsync();

            return Ok(new AuthResponseDto
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                Email = user.Email,
                Role = user.Role,
                Username = user.Username
            });

        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenRequestDto request)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null)
                return BadRequest("Невалиден токен.");

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return BadRequest("Липсва идентификатор на потребител.");

            var user = await _context.Users.FindAsync(int.Parse(userId));
            if (user == null || !_refreshHelper.IsRefreshTokenValid(user, request.RefreshToken))
                return Unauthorized("Невалиден или изтекъл refresh токен.");

            var (newAccess, newRefresh, expiry) = _refreshHelper.GenerateTokenPair(user);
            user.RefreshToken = newRefresh;
            user.RefreshTokenExpiryTime = expiry;
            await _context.SaveChangesAsync();

            return Ok(new AuthResponseDto
            {
                Token = newAccess,
                RefreshToken = newRefresh
            });
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateLifetime = false // Позволява валидиране на изтекъл токен
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                if (securityToken is not JwtSecurityToken jwtToken ||
                    !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    return null;

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
