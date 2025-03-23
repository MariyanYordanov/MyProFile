using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;
using MyProFile.Server.DTOs;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly MyProFileDbContext _context;

    public ProjectsController(MyProFileDbContext context)
    {
        _context = context;
    }

    // GET: api/Projects
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _context.Projects
            .Include(p => p.Student)
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                ScreenshotPath = p.ScreenshotPath,
                CreatedOn = p.CreatedOn,
                StudentId = p.StudentId,
                StudentName = p.Student.FullName
            })
            .ToListAsync();

        return Ok(projects);
    }

    // GET: api/Projects/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var project = await _context.Projects
            .Include(p => p.Student)
            .Where(p => p.Id == id)
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                ScreenshotPath = p.ScreenshotPath,
                CreatedOn = p.CreatedOn,
                StudentId = p.StudentId,
                StudentName = p.Student.FullName
            })
            .FirstOrDefaultAsync();

        if (project == null) return NotFound();

        return Ok(project);
    }

    // POST: api/Projects
    [HttpPost]
    public async Task<IActionResult> Create(ProjectDto dto)
    {
        var project = new Project
        {
            Title = dto.Title,
            Description = dto.Description,
            ScreenshotPath = dto.ScreenshotPath,
            CreatedOn = DateTime.UtcNow,
            StudentId = dto.StudentId
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        dto.Id = project.Id;
        dto.CreatedOn = project.CreatedOn;

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    // PUT: api/Projects/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProjectDto dto)
    {
        if (id != dto.Id) return BadRequest();

        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound();

        project.Title = dto.Title;
        project.Description = dto.Description;
        project.ScreenshotPath = dto.ScreenshotPath;
        project.StudentId = dto.StudentId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Projects/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound();

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
