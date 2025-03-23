using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Credit
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty;

    public int Value { get; set; }

    [MaxLength(100)]
    public string ValidatedBy { get; set; } = string.Empty;
     
    [MaxLength(255)]
    public string? ProofPath { get; set; }

    [ForeignKey(nameof(Student))]
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
}
