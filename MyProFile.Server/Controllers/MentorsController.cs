using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;
using MyProFile.Server.DTOs;

[ApiController]
[Route("api/[controller]")]
public class MentorsController : ControllerBase
{
    private readonly MyProFileDbContext _context;

    public MentorsController(MyProFileDbContext context)
    {
        _context = context;
    }

    // GET: api/Mentors
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var mentors = await _context.Mentors
            .Select(m => new MentorDto
            {
                Id = m.Id,
                FullName = m.FullName,
                SubjectArea = m.SubjectArea
            })
            .ToListAsync();

        return Ok(mentors);
    }


    // GET: api/Mentors/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var mentor = await _context.Mentors
            .Include(m => m.Students)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (mentor == null) return NotFound();
        return Ok(mentor);
    }

    // POST: api/Mentors
    [HttpPost]
    public async Task<IActionResult> Create(Mentor mentor)
    {
        _context.Mentors.Add(mentor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = mentor.Id }, mentor);
    }

    // PUT: api/Mentors/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Mentor mentor)
    {
        if (id != mentor.Id)
            return BadRequest();

        _context.Entry(mentor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Mentors.Any(e => e.Id == id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Mentors/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var mentor = await _context.Mentors.FindAsync(id);
        if (mentor == null) return NotFound();

        _context.Mentors.Remove(mentor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
