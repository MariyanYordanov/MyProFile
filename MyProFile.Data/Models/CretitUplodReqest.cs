using Microsoft.AspNetCore.Http;

public class CreditUploadRequest
{
    public IFormFile File { get; set; } = null!;

    public string Type { get; set; } = string.Empty;

    public int Value { get; set; }

    public string ValidatedBy { get; set; } = string.Empty;

    public int StudentId { get; set; }
}
