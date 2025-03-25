namespace MyProFile.Server.DTOs
{
    public class ProjectUploadRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int StudentId { get; set; }
        public IFormFile Screenshot { get; set; } = null!;
    }
}
