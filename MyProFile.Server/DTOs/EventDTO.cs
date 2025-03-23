namespace MyProFile.Server.DTOs;

public class EventDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public int StudentId { get; set; }

    public string? StudentName { get; set; }
}
