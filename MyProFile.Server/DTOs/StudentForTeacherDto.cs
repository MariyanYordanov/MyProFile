namespace MyProFile.Server.DTOs;

public class StudentForTeacherDto
{
    public StudentDto Student { get; set; } = new StudentDto();
    public int PendingCount { get; set; }
}
