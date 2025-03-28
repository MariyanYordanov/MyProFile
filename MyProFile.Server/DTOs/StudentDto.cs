namespace MyProFile.Server.DTOs;

public class StudentDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty!;
    public string Class { get; set; } = string.Empty!;
    public string Speciality { get; set; } = string.Empty!;
    public double AverageGrade { get; set; }
    public string Rating { get; set; } = string.Empty!;
    public string? MentorName { get; set; }
    public string? ProfilePicturePath { get; set; }

}
