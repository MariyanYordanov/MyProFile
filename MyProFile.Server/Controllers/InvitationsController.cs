using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;
using MyProFile.Server.Utilities;

namespace MyProFile.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Само админ може да праща покани
    public class InvitationsController : ControllerBase
    {
        private readonly MyProFileDbContext _context;
        private readonly MailHelper _mailHelper;

        public InvitationsController(MyProFileDbContext context, MailHelper mailHelper)
        {
            _context = context;
            _mailHelper = mailHelper;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendInvitation([FromBody] InvitationRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Role))
                return BadRequest("Email и роля са задължителни.");

            var existing = await _context.Invitations.FirstOrDefaultAsync(i => i.Email == dto.Email && !i.IsUsed);
            if (existing != null)
                return BadRequest("Покана вече е изпратена на този имейл и не е използвана.");

            var token = Guid.NewGuid().ToString();

            var invitation = new Invitation
            {
                Email = dto.Email,
                Role = dto.Role,
                Token = token,
                SentAt = DateTime.UtcNow,
                Expiration = DateTime.UtcNow.AddHours(24),
                IsUsed = false
            };


            _context.Invitations.Add(invitation);
            await _context.SaveChangesAsync();

            await _mailHelper.SendInvitationEmail(dto.Email, token, dto.Role);

            return Ok("Поканата е изпратена успешно.");
        }
    }
}
