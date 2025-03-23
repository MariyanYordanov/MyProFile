using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;
using MyProFile.Server.DTOs;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly MyProFileDbContext _context;

    public EventsController(MyProFileDbContext context)
    {
        _context = context;
    }

    // GET: api/Events
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _context.Events
            .Include(e => e.Student)
            .Select(e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Date = e.Date,
                StudentId = e.StudentId,
                StudentName = e.Student.FullName
            })
            .ToListAsync();

        return Ok(events);
    }

    // GET: api/Events/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var e = await _context.Events
            .Include(e => e.Student)
            .Where(e => e.Id == id)
            .Select(e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Date = e.Date,
                StudentId = e.StudentId,
                StudentName = e.Student.FullName
            })
            .FirstOrDefaultAsync();

        if (e == null) return NotFound();

        return Ok(e);
    }

    // POST: api/Events
    [HttpPost]
    public async Task<IActionResult> Create(EventDto dto)
    {
        var e = new Event
        {
            Title = dto.Title,
            Description = dto.Description,
            Date = dto.Date == default ? DateTime.UtcNow : dto.Date,
            StudentId = dto.StudentId
        };

        _context.Events.Add(e);
        await _context.SaveChangesAsync();

        dto.Id = e.Id;
        dto.Date = e.Date;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    // PUT: api/Events/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EventDto dto)
    {
        if (id != dto.Id) return BadRequest();

        var e = await _context.Events.FindAsync(id);
        if (e == null) return NotFound();

        e.Title = dto.Title;
        e.Description = dto.Description;
        e.Date = dto.Date;
        e.StudentId = dto.StudentId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Events/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var e = await _context.Events.FindAsync(id);
        if (e == null) return NotFound();

        _context.Events.Remove(e);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
