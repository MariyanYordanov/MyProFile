using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProFile.Data.Models;
namespace MyProFile.Data;

public class MyProFileDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public MyProFileDbContext(DbContextOptions<MyProFileDbContext> options)
        : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Credit> Credits { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Sanction> Sanctions { get; set; }
    public DbSet<Interest> Interests { get; set; }
    public DbSet<Invitation> Invitations { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole<int>>().HasData(
    new IdentityRole<int> { Id = 1, Name = "admin", NormalizedName = "ADMIN" },
    new IdentityRole<int> { Id = 2, Name = "teacher", NormalizedName = "TEACHER" },
    new IdentityRole<int> { Id = 3, Name = "student", NormalizedName = "STUDENT" }
);


        var hasher = new PasswordHasher<User>();

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                UserName = "admin1",
                NormalizedUserName = "ADMIN1",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin123!"),
                SecurityStamp = Guid.NewGuid().ToString(),
                Role = "admin"
            },
            new User
            {
                Id = 2,
                UserName = "teacher1",
                NormalizedUserName = "TEACHER1",
                Email = "teacher@example.com",
                NormalizedEmail = "TEACHER@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Teacher123!"),
                SecurityStamp = Guid.NewGuid().ToString(),
                Role = "teacher"
            },
            new User
            {
                Id = 3,
                UserName = "student1",
                NormalizedUserName = "STUDENT1",
                Email = "student@example.com",
                NormalizedEmail = "STUDENT@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Student123!"),
                SecurityStamp = Guid.NewGuid().ToString(),
                Role = "student"
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
                  .WithMany(u => u.Mentees)
                  .HasForeignKey(s => s.MentorId)
                  .OnDelete(DeleteBehavior.Restrict);
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
