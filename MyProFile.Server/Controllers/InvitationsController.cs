using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;
using MyProFile.Server.Utilities;

namespace MyProFile.Server.Controllers
{
    // Само админ може да праща покани
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationsController : ControllerBase
    {
        private readonly MyProFileDbContext _context; // ❗ ТОВА ЛИПСВАШЕ
        private readonly MailHelper _mailHelper;

        public InvitationsController(MyProFileDbContext context, MailHelper mailHelper)
        {
            _context = context;
            _mailHelper = mailHelper;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("send")]
        public async Task<IActionResult> SendInvitation([FromBody] InvitationRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Role))
                return BadRequest("Email и роля са задължителни.");

            var existing = await _context.Invitations.FirstOrDefaultAsync(i => i.Email == dto.Email && !i.IsUsed);
            if (existing != null)
                return BadRequest("Покана вече е изпратена на този имейл и все още е активна.");

            var token = Guid.NewGuid().ToString();
            var invitation = new Invitation
            {
                Email = dto.Email,
                Role = dto.Role,
                Token = token,
                SentAt = DateTime.UtcNow,
                Expiration = DateTime.UtcNow.AddDays(2),
                IsUsed = false
            };

            _context.Invitations.Add(invitation);
            await _context.SaveChangesAsync();

            // Изпращаме имейл с линк към формата за регистрация
            await _mailHelper.SendInvitationEmail(dto.Email, token, dto.Role);

            return Ok("✅ Поканата е изпратена успешно.");
        }

        [HttpGet("validate")]
        public async Task<IActionResult> ValidateToken([FromQuery] string token)
        {
            var invitation = await _context.Invitations
                .FirstOrDefaultAsync(i => i.Token == token && !i.IsUsed && i.Expiration > DateTime.UtcNow);

            if (invitation == null)
                return NotFound("Невалиден или изтекъл токен.");

            return Ok(new { email = invitation.Email, role = invitation.Role });
        }
    }
}
