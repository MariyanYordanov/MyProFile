namespace MyProFile.Server.DTOs;

public class InterestDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int StudentId { get; set; }

    public string? StudentName { get; set; }
}
