using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;
using MyProFile.Server.DTOs;

//[Authorize]
[AllowAnonymous]// за тестване на изпращане на мейл
[ApiController]
[Route("api/[controller]")]
public class InterestsController : ControllerBase
{
    private readonly MyProFileDbContext _context;

    public InterestsController(MyProFileDbContext context)
    {
        _context = context;
    }

    // GET: api/Interests
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var interests = await _context.Interests
            .Include(i => i.Student)
            .Select(i => new InterestDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                StudentId = i.StudentId,
                StudentName = i.Student.FullName
            })
            .ToListAsync();

        return Ok(interests);
    }

    // GET: api/Interests/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var interest = await _context.Interests
            .Include(i => i.Student)
            .Where(i => i.Id == id)
            .Select(i => new InterestDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                StudentId = i.StudentId,
                StudentName = i.Student.FullName
            })
            .FirstOrDefaultAsync();

        if (interest == null) return NotFound();

        return Ok(interest);
    }

    // POST: api/Interests
    [HttpPost]
    public async Task<IActionResult> Create(InterestDto dto)
    {
        var interest = new Interest
        {
            Name = dto.Name,
            Description = dto.Description,
            StudentId = dto.StudentId
        };

        _context.Interests.Add(interest);
        await _context.SaveChangesAsync();

        dto.Id = interest.Id;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    // PUT: api/Interests/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, InterestDto dto)
    {
        if (id != dto.Id) return BadRequest();

        var interest = await _context.Interests.FindAsync(id);
        if (interest == null) return NotFound();

        interest.Name = dto.Name;
        interest.Description = dto.Description;
        interest.StudentId = dto.StudentId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Interests/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var interest = await _context.Interests.FindAsync(id);
        if (interest == null) return NotFound();

        _context.Interests.Remove(interest);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
