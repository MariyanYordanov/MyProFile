using System.ComponentModel.DataAnnotations;

namespace MyProFile.Server.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Имейлът е задължителен")]
        [EmailAddress(ErrorMessage = "Невалиден имейл")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Потребителското име е задължително")]
        [MinLength(3, ErrorMessage = "Минимална дължина на потребителското име е 3 символа")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Паролата е задължителна")]
        [MinLength(6, ErrorMessage = "Минимална дължина на паролата е 6 символа")]
        public string Password { get; set; } = string.Empty;

        // За бъдещо разширение: по покана, с роля, клас и специалност
        public string? Role { get; set; }
        public string? Class { get; set; }
        public string? Speciality { get; set; }
    }
}
