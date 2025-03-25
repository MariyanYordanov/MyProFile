namespace MyProFile.Server.DTOs
{
    public class AchievementUploadRequest
    {
        public IFormFile File { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public string? Details { get; set; }

        // ISO format
        public string Date { get; set; } = string.Empty;
        public int StudentId { get; set; }
    }
}
