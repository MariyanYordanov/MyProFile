using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;
using MyProFile.Server.DTOs;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SanctionsController : ControllerBase
{
    private readonly MyProFileDbContext _context;

    public SanctionsController(MyProFileDbContext context)
    {
        _context = context;
    }

    // GET: api/Sanctions
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sanctions = await _context.Sanctions
            .Include(s => s.Student)
            .Select(s => new SanctionDto
            {
                Id = s.Id,
                Reason = s.Reason,
                Notes = s.Notes,
                Date = s.Date,
                StudentId = s.StudentId,
                StudentName = s.Student.FullName
            })
            .ToListAsync();

        return Ok(sanctions);
    }

    // GET: api/Sanctions/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var s = await _context.Sanctions
            .Include(s => s.Student)
            .Where(s => s.Id == id)
            .Select(s => new SanctionDto
            {
                Id = s.Id,
                Reason = s.Reason,
                Notes = s.Notes,
                Date = s.Date,
                StudentId = s.StudentId,
                StudentName = s.Student.FullName
            })
            .FirstOrDefaultAsync();

        if (s == null) return NotFound();

        return Ok(s);
    }

    // POST: api/Sanctions
    [HttpPost]
    public async Task<IActionResult> Create(SanctionDto dto)
    {
        var s = new Sanction
        {
            Reason = dto.Reason,
            Notes = dto.Notes,
            Date = dto.Date == default ? DateTime.UtcNow : dto.Date,
            StudentId = dto.StudentId
        };

        _context.Sanctions.Add(s);
        await _context.SaveChangesAsync();

        dto.Id = s.Id;
        dto.Date = s.Date;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    // PUT: api/Sanctions/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SanctionDto dto)
    {
        if (id != dto.Id) return BadRequest();

        var s = await _context.Sanctions.FindAsync(id);
        if (s == null) return NotFound();

        s.Reason = dto.Reason;
        s.Notes = dto.Notes;
        s.Date = dto.Date;
        s.StudentId = dto.StudentId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Sanctions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var s = await _context.Sanctions.FindAsync(id);
        if (s == null) return NotFound();

        _context.Sanctions.Remove(s);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
