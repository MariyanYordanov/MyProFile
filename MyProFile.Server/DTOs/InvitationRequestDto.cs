using System.ComponentModel.DataAnnotations;

public class InvitationRequestDto
{
    [Required(ErrorMessage = "Имейлът е задължителен.")]
    [EmailAddress(ErrorMessage = "Невалиден формат на имейл.")]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ролята е задължителна.")]
    [StringLength(50, ErrorMessage = "Ролята не може да надвишава 50 символа.")]
    public string Role { get; set; } = string.Empty;
}
