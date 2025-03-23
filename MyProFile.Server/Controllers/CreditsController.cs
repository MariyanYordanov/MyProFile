using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;
using MyProFile.Server.DTOs;

[ApiController]
[Route("api/[controller]")]
public class CreditsController : ControllerBase
{
    private readonly MyProFileDbContext _context;

    public CreditsController(MyProFileDbContext context)
    {
        _context = context;
    }

    // GET: api/Credits
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

    // GET: api/Credits/5
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

    // POST: api/Credits
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

    // PUT: api/Credits/5
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

    // DELETE: api/Credits/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var credit = await _context.Credits.FindAsync(id);
        if (credit == null) return NotFound();

        _context.Credits.Remove(credit);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
