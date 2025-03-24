using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;
using MyProFile.Server.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AchievementsController : ControllerBase
{
    private readonly MyProFileDbContext _context; //Dependences injention

    public AchievementsController(MyProFileDbContext context)
    {
        _context = context;
    }

    // GET: api/Achievements
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var achievements = await _context.Achievements
            .Include(a => a.Student)
            .Select(a => new AchievementDto
            {
                Id = a.Id,
                Title = a.Title,
                Details = a.Details,
                Date = a.Date,
                StudentId = a.StudentId,
                StudentName = a.Student.FullName
            })
            .ToListAsync();

        return Ok(achievements);
    }

    // GET: api/Achievements/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var achievement = await _context.Achievements
            .Include(a => a.Student)
            .Where(a => a.Id == id)
            .Select(a => new AchievementDto
            {
                Id = a.Id,
                Title = a.Title,
                Details = a.Details,
                Date = a.Date,
                StudentId = a.StudentId,
                StudentName = a.Student.FullName
            })
            .FirstOrDefaultAsync();

        if (achievement == null) return NotFound();

        return Ok(achievement);
    }

    // POST: api/Achievements
    [HttpPost]
    public async Task<IActionResult> Create(AchievementDto dto)
    {
        var achievement = new Achievement
        {
            Title = dto.Title,
            Details = dto.Details,
            Date = dto.Date == default ? DateTime.UtcNow : dto.Date,
            StudentId = dto.StudentId
        };

        _context.Achievements.Add(achievement);
        await _context.SaveChangesAsync();

        dto.Id = achievement.Id;
        dto.Date = achievement.Date;

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    // PUT: api/Achievements/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AchievementDto dto)
    {
        if (id != dto.Id) return BadRequest();

        var achievement = await _context.Achievements.FindAsync(id);
        if (achievement == null) return NotFound();

        achievement.Title = dto.Title;
        achievement.Details = dto.Details;
        achievement.Date = dto.Date;
        achievement.StudentId = dto.StudentId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Achievements/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var achievement = await _context.Achievements.FindAsync(id);
        if (achievement == null) return NotFound();

        _context.Achievements.Remove(achievement);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
