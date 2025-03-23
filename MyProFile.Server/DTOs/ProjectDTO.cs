namespace MyProFile.Server.DTOs;

public class ProjectDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? ScreenshotPath { get; set; }

    public DateTime CreatedOn { get; set; }

    public int StudentId { get; set; }

    public string? StudentName { get; set; }
}
