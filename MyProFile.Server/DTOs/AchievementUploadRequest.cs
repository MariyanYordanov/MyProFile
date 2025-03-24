namespace MyProFile.Server.DTOs
{
    public class AchievementUploadRequest
    {
        public IFormFile File { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public string? Details { get; set; }
        public string Date { get; set; } = string.Empty; // ISO format
        public int StudentId { get; set; }
    }
}
