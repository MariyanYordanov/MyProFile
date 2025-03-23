namespace MyProFile.Server.DTOs;

public class CreditDto
{
    public int Id { get; set; }

    // Пример: "Професия", "Аз и другите", "Мислене"
    public string Type { get; set; } = string.Empty;

    public int Value { get; set; }

    public string ValidatedBy { get; set; } = string.Empty;

    public string? ProofPath { get; set; }

    public int StudentId { get; set; }

    public string? StudentName { get; set; }
}
