using Microsoft.EntityFrameworkCore;
using MyProFile.Data.Models;
namespace MyProFile.Data;

public class MyProFileDbContext : DbContext
{
    public MyProFileDbContext(DbContextOptions<MyProFileDbContext> options)
        : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Credit> Credits { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Mentor> Mentors { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Sanction> Sanctions { get; set; }
    public DbSet<Interest> Interests { get; set; }
    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent конфигурации 
        modelBuilder.Entity<Mentor>().HasData(
            new Mentor { Id = 1, FullName = "Васил Петров", SubjectArea = "Програмиране" },
            new Mentor { Id = 2, FullName = "Мария Николова", SubjectArea = "UI/UX дизайн" }
        );

        modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@myprofile.bg",
                    Username = "Администратор",
                    PasswordHash = "$2a$11$KIXTpRZn70aMwdyHf4sFaeQBXoRFBPCE.5sn1ItMnvW8b8VfZ5E.m", // "admin1234"
                    Role = "Admin"
                }
            );

        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = 1,
                FullName = "Иван Иванов",
                Class = "10А",
                Speciality = "Софтуерни технологии",
                AverageGrade = 5.40,
                Rating = "напреднал",
                MentorId = 1,
                ProfilePicturePath = "ivan.jpg"
            },
            new Student
            {
                Id = 2,
                FullName = "Елица Георгиева",
                Class = "10Б",
                Speciality = "Графичен дизайн",
                AverageGrade = 5.90,
                Rating = "начинаещ",
                MentorId = 2,
                ProfilePicturePath = "elitsa.jpg"
            }
        );

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.FullName).IsRequired().HasMaxLength(100);
            entity.Property(s => s.Class).HasMaxLength(20);
            entity.Property(s => s.Speciality).HasMaxLength(100);
            entity.Property(s => s.Rating).HasMaxLength(50);

            entity.HasOne(s => s.Mentor)
                  .WithMany(m => m.Students)
                  .HasForeignKey(s => s.MentorId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Project>(entity =>
        {


            entity.HasOne(p => p.Student)
                  .WithMany(s => s.Projects)
                  .HasForeignKey(p => p.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Event>().HasData(
            new Event
            {
                Id = 1,
                Title = "Участие в ученическа конференция",
                Description = "Изнесена презентация на тема ИИ в образованието",
                Date = new DateTime(2024, 3, 15),
                StudentId = 1
            },
            new Event
            {
                Id = 2,
                Title = "Стаж в ИТ фирма",
                Description = "2 седмици практика в Software Company",
                Date = new DateTime(2024, 6, 10),
                StudentId = 2
            }
        );

    }

}
