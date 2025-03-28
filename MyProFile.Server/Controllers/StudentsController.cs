using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;
using MyProFile.Server.DTOs;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly MyProFileDbContext _context;

    public StudentsController(MyProFileDbContext context)
    {
        _context = context;
    }

    // GET: api/Students
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _context.Students
            .Include(s => s.Mentor)
            .Select(s => new StudentDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Class = s.Class,
                Speciality = s.Speciality,
                AverageGrade = s.AverageGrade,
                Rating = s.Rating,
                MentorName = s.Mentor != null ? s.Mentor.FullName : null
            })
            .ToListAsync();

        return Ok(students);
    }


    // GET: api/Students/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _context.Students
            .Include(s => s.Mentor)
            .Include(s => s.Projects)
            .Include(s => s.Credits)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null) return NotFound();

        return Ok(student);
    }

    // POST: api/Students
    [HttpPost]
    public async Task<IActionResult> Create(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
    }

    // PUT: api/Students/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Student student)
    {
        if (id != student.Id)
            return BadRequest();

        _context.Entry(student).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Students.Any(e => e.Id == id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Students/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpPost("{id}/profile-picture")]
    [RequestSizeLimit(5_000_000)]
    public async Task<IActionResult> UploadProfilePicture(int id, [FromForm] IFormFile file)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            return NotFound();

        if (file == null || file.Length == 0)
            return BadRequest("Файлът е празен.");

        var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profiles");
        if (!Directory.Exists(uploadsPath))
            Directory.CreateDirectory(uploadsPath);

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var fullPath = Path.Combine(uploadsPath, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        student.ProfilePicturePath = $"/profiles/{fileName}";
        await _context.SaveChangesAsync();

        return Ok(new { student.Id, student.ProfilePicturePath });
    }

    [HttpGet("{id}/overview")]
    public async Task<IActionResult> GetOverview(int id)
    {
        var student = await _context.Students
            .Include(s => s.Mentor)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null)
            return NotFound();

        var studentDto = new StudentDto
        {
            Id = student.Id,
            FullName = student.FullName,
            Class = student.Class,
            Speciality = student.Speciality,
            AverageGrade = student.AverageGrade,
            Rating = student.Rating,
            MentorName = student.Mentor?.FullName,
            ProfilePicturePath = student.ProfilePicturePath
        };

        var credits = await _context.Credits
            .Where(c => c.StudentId == id)
            .Select(c => new CreditDto
            {
                Id = c.Id,
                Type = c.Type,
                Value = c.Value,
                ValidatedBy = c.ValidatedBy,
                ProofPath = c.ProofPath,
                StudentId = c.StudentId,
                StudentName = c.Student.FullName
            }).ToListAsync();

        var achievements = await _context.Achievements
            .Where(a => a.StudentId == id)
            .Select(a => new AchievementDto
            {
                Id = a.Id,
                Title = a.Title,
                Details = a.Details,
                Date = a.Date,
                StudentId = a.StudentId,
                StudentName = a.Student.FullName
            }).ToListAsync();

        var goals = await _context.Goals
            .Where(g => g.StudentId == id)
            .Select(g => new GoalDto
            {
                Id = g.Id,
                Area = g.Area,
                Description = g.Description,
                StudentId = g.StudentId
            }).ToListAsync();

        var events = await _context.Events
            .Where(e => e.StudentId == id)
            .Select(e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Date = e.Date,
                StudentId = e.StudentId
            }).ToListAsync();

        var sanctions = await _context.Sanctions
            .Where(s => s.StudentId == id)
            .Select(s => new SanctionDto
            {
                Id = s.Id,
                Reason = s.Reason,
                Notes = s.Notes,
                Date = s.Date,
                StudentId = s.StudentId
            }).ToListAsync();

        var interests = await _context.Interests
            .Where(i => i.StudentId == id)
            .Select(i => new InterestDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                StudentId = i.StudentId
            }).ToListAsync();

        var stats = new StatisticsDto
        {
            TotalCredits = credits.Sum(c => c.Value),
            AchievementsCount = achievements.Count,
            EventsCount = events.Count,
            GoalsCount = goals.Count,
            SanctionsCount = sanctions.Count,
            InterestsCount = interests.Count
        };

        var overview = new StudentOverviewDto
        {
            Student = studentDto,
            Credits = credits,
            Achievements = achievements,
            Goals = goals,
            Events = events,
            Sanctions = sanctions,
            Interests = interests,
            Stats = stats
        };

        return Ok(overview);
    }
}
