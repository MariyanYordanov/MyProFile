using System.ComponentModel.DataAnnotations;

namespace MyProFile.Server.DTOs
{
    public class ProfilePictureUploadRequest
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
