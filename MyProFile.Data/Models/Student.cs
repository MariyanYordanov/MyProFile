using MyProFile.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Student
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(20)]
    public string Class { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Speciality { get; set; } = string.Empty;

    [MaxLength(255)]
    public string ProfilePicturePath { get; set; } = string.Empty;

    public double AverageGrade { get; set; }

    [MaxLength(50)]
    public string Rating { get; set; } = string.Empty;

   
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<Credit> Credits { get; set; } = new List<Credit>();
    public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    public ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();

    [ForeignKey(nameof(Mentor))]
    public int? MentorId { get; set; }
    public User? Mentor { get; set; }
}
