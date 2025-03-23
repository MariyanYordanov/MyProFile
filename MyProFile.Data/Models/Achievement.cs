using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Achievement
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Details { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(Student))]
    public int StudentId { get; set; }

    public Student Student { get; set; } = null!;
}

