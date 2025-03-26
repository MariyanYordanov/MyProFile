namespace MyProFile.Server.DTOs
{
    public class EventUploadRequest
    {
        public IFormFile File { get; set; } = default!;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public int StudentId { get; set; }
    }
}
