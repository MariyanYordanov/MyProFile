using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data;
using MyProFile.Server.DTOs;

namespace MyProFile.Server.Controllers;

[ApiController]
[Route("api/teachers")]
[Authorize(Roles = "teacher")]
public class TeacherController : ControllerBase
{
    private readonly MyProFileDbContext _context;

    public TeacherController(MyProFileDbContext context)
    {
        _context = context;
    }

    [HttpGet("students")]
    public async Task<ActionResult<IEnumerable<StudentForTeacherDto>>> GetStudents()
    {
        var teacherEmail = User.Identity?.Name;

        if (string.IsNullOrEmpty(teacherEmail))
        {
            Console.WriteLine("[TeacherController] ❌ Липсващ имейл!");
            return Unauthorized("Липсващ имейл");
        }

        var teacher = await _context.Users.FirstOrDefaultAsync(u => u.Email == teacherEmail);
        if (teacher == null)
        {
            Console.WriteLine("[TeacherController] ❌ Учителят не е намерен в базата!");
            return Unauthorized("Учителят не е намерен");
        }

        var students = await _context.Students
            .Where(s => s.MentorId == teacher.Id)
            .Include(s => s.Goals)
            .Include(s => s.Achievements)
            .Select(s => new StudentForTeacherDto
            {
                Student = new StudentDto
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Class = s.Class,
                    Speciality = s.Speciality,
                    AverageGrade = s.AverageGrade,
                    Rating = s.Rating,
                    MentorName = teacher.FullName,
                    ProfilePicturePath = s.ProfilePicturePath
                },
                PendingCount = s.Goals.Count(g => !g.IsVerified) + s.Achievements.Count(a => !a.IsVerified)
            })
            .ToListAsync();

        return Ok(students);
    }
}
