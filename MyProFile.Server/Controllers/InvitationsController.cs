using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;
using MyProFile.Server.Utilities;

namespace MyProFile.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationsController : ControllerBase
    {
        private readonly MyProFileDbContext _context;

        public InvitationsController(MyProFileDbContext context)
        {
            _context = context;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendInvitation([FromBody] InvitationRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Role))
                return BadRequest("Email и роля са задължителни.");

            var existing = await _context.Invitations.FirstOrDefaultAsync(i => i.Email == dto.Email);
            if (existing != null)
                return BadRequest("Покана вече е изпратена на този имейл.");

            var token = Guid.NewGuid().ToString();
            var invitation = new Invitation
            {
                Email = dto.Email,
                Role = dto.Role,
                Token = token,
                SentAt = DateTime.UtcNow,
                IsUsed = false
            };

            _context.Invitations.Add(invitation);
            await _context.SaveChangesAsync();

            await MailHelper.SendInvitationEmail(dto.Email, token, dto.Role);

            return Ok("Поканата е изпратена успешно.");
        }
    }
}
