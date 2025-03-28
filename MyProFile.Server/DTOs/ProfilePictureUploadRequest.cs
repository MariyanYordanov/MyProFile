public class ProfilePictureUploadRequest
{
    public int StudentId { get; set; }
    public IFormFile File { get; set; } = null!;
}
