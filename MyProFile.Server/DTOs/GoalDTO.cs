namespace MyProFile.Server.DTOs;

public class GoalDto
{
    public int Id { get; set; }

    // Пример: "Професия", "Мислене", "Аз и другите", "Общност"
    public string Area { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int StudentId { get; set; }

    public string? StudentName { get; set; }
}
