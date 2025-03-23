namespace MyProFile.Server.DTOs;

public class AchievementDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Details { get; set; }

    public DateTime Date { get; set; }

    public int StudentId { get; set; }

    public string? StudentName { get; set; }
}
