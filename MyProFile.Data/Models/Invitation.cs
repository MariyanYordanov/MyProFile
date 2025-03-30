using System.ComponentModel.DataAnnotations;

public class Invitation
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Role { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Token { get; set; } = string.Empty;

    [Required]
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime Expiration { get; set; }

    public bool IsUsed { get; set; } = false;
}
