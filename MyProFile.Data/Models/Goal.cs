using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Goal
{
    [Key]
    public int Id { get; set; }

    // „Професия“, „Мислене“, „Аз и другите“ и т.н.
    [Required]
    [MaxLength(100)]
    public string Area { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [ForeignKey(nameof(Student))]
    public int StudentId { get; set; }

    public Student Student { get; set; } = null!;

    public bool IsVerified { get; set; } = false;

}
