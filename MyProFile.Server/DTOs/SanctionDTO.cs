namespace MyProFile.Server.DTOs;

public class SanctionDto
{
    public int Id { get; set; }

    public string Reason { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public string? Notes { get; set; }

    public int StudentId { get; set; }

    public string? StudentName { get; set; }
}
