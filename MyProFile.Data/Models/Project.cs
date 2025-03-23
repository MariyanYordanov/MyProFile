using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

public class Project
{
    public Project()
    {
        CreatedOn = DateTime.UtcNow; // Добавя автоматично време
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [MaxLength(255)]
    public string? ScreenshotPath { get; set; }

    [DefaultValue("CURRENT_TIMESTAMP")]
    public DateTime CreatedOn { get; set; }

    [ForeignKey(nameof(Student))]
    public int StudentId { get; set; }

    public Student Student { get; set; } = null!;
}
