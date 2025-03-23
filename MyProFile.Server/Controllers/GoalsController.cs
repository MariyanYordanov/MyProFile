using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;
using MyProFile.Server.DTOs;

[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{
    private readonly MyProFileDbContext _context;

    public GoalsController(MyProFileDbContext context)
    {
        _context = context;
    }

    // GET: api/Goals
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var goals = await _context.Goals
            .Include(g => g.Student)
            .Select(g => new GoalDto
            {
                Id = g.Id,
                Area = g.Area,
                Description = g.Description,
                StudentId = g.StudentId,
                StudentName = g.Student.FullName
            })
            .ToListAsync();

        return Ok(goals);
    }

    // GET: api/Goals/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var goal = await _context.Goals
            .Include(g => g.Student)
            .Where(g => g.Id == id)
            .Select(g => new GoalDto
            {
                Id = g.Id,
                Area = g.Area,
                Description = g.Description,
                StudentId = g.StudentId,
                StudentName = g.Student.FullName
            })
            .FirstOrDefaultAsync();

        if (goal == null) return NotFound();

        return Ok(goal);
    }

    // POST: api/Goals
    [HttpPost]
    public async Task<IActionResult> Create(GoalDto dto)
    {
        var goal = new Goal
        {
            Area = dto.Area,
            Description = dto.Description,
            StudentId = dto.StudentId
        };

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        dto.Id = goal.Id;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    // PUT: api/Goals/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, GoalDto dto)
    {
        if (id != dto.Id) return BadRequest();

        var goal = await _context.Goals.FindAsync(id);
        if (goal == null) return NotFound();

        goal.Area = dto.Area;
        goal.Description = dto.Description;
        goal.StudentId = dto.StudentId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Goals/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var goal = await _context.Goals.FindAsync(id);
        if (goal == null) return NotFound();

        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
