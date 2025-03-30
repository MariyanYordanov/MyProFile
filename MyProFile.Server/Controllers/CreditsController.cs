using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;
using MyProFile.Server.DTOs;

namespace MyProFile.Server.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CreditsController : ControllerBase
    {
        private readonly MyProFileDbContext _context;

        public CreditsController(MyProFileDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var credits = await _context.Credits
                .Include(c => c.Student)
                .Select(c => new CreditDto
                {
                    Id = c.Id,
                    Type = c.Type,
                    Value = c.Value,
                    ValidatedBy = c.ValidatedBy,
                    ProofPath = c.ProofPath,
                    StudentId = c.StudentId,
                    StudentName = c.Student.FullName
                })
                .ToListAsync();

            return Ok(credits);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var credit = await _context.Credits
                .Include(c => c.Student)
                .Where(c => c.Id == id)
                .Select(c => new CreditDto
                {
                    Id = c.Id,
                    Type = c.Type,
                    Value = c.Value,
                    ValidatedBy = c.ValidatedBy,
                    ProofPath = c.ProofPath,
                    StudentId = c.StudentId,
                    StudentName = c.Student.FullName
                })
                .FirstOrDefaultAsync();

            if (credit == null) return NotFound();

            return Ok(credit);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreditDto dto)
        {
            var credit = new Credit
            {
                Type = dto.Type,
                Value = dto.Value,
                ValidatedBy = dto.ValidatedBy,
                ProofPath = dto.ProofPath,
                StudentId = dto.StudentId
            };

            _context.Credits.Add(credit);
            await _context.SaveChangesAsync();

            dto.Id = credit.Id;
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreditDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var credit = await _context.Credits.FindAsync(id);
            if (credit == null) return NotFound();

            credit.Type = dto.Type;
            credit.Value = dto.Value;
            credit.ValidatedBy = dto.ValidatedBy;
            credit.ProofPath = dto.ProofPath;
            credit.StudentId = dto.StudentId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var credit = await _context.Credits.FindAsync(id);
            if (credit == null) return NotFound();

            _context.Credits.Remove(credit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("upload")]
        [RequestSizeLimit(10_000_000)]
        public async Task<IActionResult> UploadCreditWithFile([FromForm] CreditUploadRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("Файлът е празен.");

            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var uniqueName = $"{Guid.NewGuid()}_{request.File.FileName}";
            var fullPath = Path.Combine(uploadsPath, uniqueName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            var credit = new Credit
            {
                Type = request.Type,
                Value = request.Value,
                ValidatedBy = request.ValidatedBy,
                ProofPath = $"/uploads/{uniqueName}",
                StudentId = request.StudentId
            };

            _context.Credits.Add(credit);
            await _context.SaveChangesAsync();

            return Ok(new { id = credit.Id, proofPath = credit.ProofPath });
        }
    }
}
