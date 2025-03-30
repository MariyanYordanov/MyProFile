using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyProFile.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyProFile.Server.Utilities
{
    public class RefreshTokenHelper
    {
        private readonly IConfiguration _config;

        public RefreshTokenHelper(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            Console.WriteLine("JWT KEY: " + _config["Jwt:Key"]);
            Console.WriteLine("JWT KEY LENGTH: " + Encoding.UTF8.GetBytes(_config["Jwt:Key"]).Length * 8 + " bits");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15), // short expiry
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public (string accessToken, string refreshToken, DateTime refreshExpiry) GenerateTokenPair(User user)
        {
            var refreshToken = GenerateRefreshToken();
            var accessToken = GenerateAccessToken(user);
            var refreshExpiry = DateTime.UtcNow.AddDays(7);
            return (accessToken, refreshToken, refreshExpiry);
        }

        public bool IsRefreshTokenValid(User user, string refreshToken)
        {
            return user.RefreshToken == refreshToken &&
                   user.RefreshTokenExpiryTime.HasValue &&
                   user.RefreshTokenExpiryTime.Value > DateTime.UtcNow;
        }
    }
}
