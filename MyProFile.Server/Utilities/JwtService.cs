using Microsoft.IdentityModel.Tokens;
using MyProFile.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyProFile.Server.Utilities
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),

                // 👇 Коректните claim-и, съвместими с JwtBearer настройките
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", user.Email ?? ""),
                new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", user.Role ?? ""),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
