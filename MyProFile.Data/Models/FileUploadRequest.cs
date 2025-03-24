using Microsoft.AspNetCore.Http;

public class FileUploadRequest
{
    public IFormFile File { get; set; } = null!;
}
