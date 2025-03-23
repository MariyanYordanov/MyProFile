using System.ComponentModel.DataAnnotations;

public class Mentor
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string SubjectArea { get; set; } = string.Empty;

    public ICollection<Student> Students { get; set; } = new List<Student>();
}
