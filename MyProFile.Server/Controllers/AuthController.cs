using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProFile.Data.Models;
using MyProFile.Server.DTOs;
using MyProFile.Server.Utilities;

namespace MyProFile.Server.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly JwtService _jwtService;

    public AuthController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        JwtService jwtService)
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
        var token = _jwtService.GenerateToken(user);

        return Ok(new { token });
    }
}
