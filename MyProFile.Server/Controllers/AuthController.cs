using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProFile.Data.Models;
using MyProFile.Server.DTOs;
using MyProFile.Server.Utilities;
using System.IdentityModel.Tokens.Jwt;

namespace MyProFile.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly JwtService _jwtService;

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
        {
            return Unauthorized("Невалидни данни за вход");
        }

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        // Генериране на access и refresh токени
        var accessToken = _jwtService.GenerateToken(user, ipAddress);
        var refreshToken = _jwtService.GenerateRefreshToken(ipAddress);

        // Запазване на refresh токена към потребителя
        user.RefreshTokens.Add(refreshToken);
        await _userManager.UpdateAsync(user);

        // Връщане на двата токена към клиента
        return Ok(new
        {
            token = accessToken,
            refreshToken = refreshToken.Token
        });
    }


    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshDto dto)
    {
        var principal = _jwtService.GetPrincipalFromExpiredToken(dto.AccessToken);
        if (principal == null)
            return BadRequest("Invalid access token");

        var email = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
        if (email == null)
            return BadRequest("Email claim missing");

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return Unauthorized("User not found");

        var existingToken = user.RefreshTokens.FirstOrDefault(x => x.Token == dto.RefreshToken);
        if (existingToken == null || !existingToken.IsActive)
            return Unauthorized("Invalid refresh token");

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var newToken = _jwtService.GenerateToken(user, ipAddress);
        var newRefreshToken = _jwtService.GenerateRefreshToken(ipAddress);

        existingToken.Revoked = DateTime.UtcNow;
        existingToken.RevokedByIp = ipAddress;
        existingToken.ReplacedByToken = newRefreshToken.Token;

        user.RefreshTokens.Add(newRefreshToken);
        await _userManager.UpdateAsync(user);

        return Ok(new
        {
            token = newToken,
            refreshToken = newRefreshToken.Token
        });
    }
}
