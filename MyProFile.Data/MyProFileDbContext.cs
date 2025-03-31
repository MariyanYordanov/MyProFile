using Microsoft.EntityFrameworkCore;
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
    public DbSet<Event> Events { get; set; }
    public DbSet<Sanction> Sanctions { get; set; }
    public DbSet<Interest> Interests { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Invitation> Invitations { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent конфигурации 
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin1",
                Email = "admin@example.com",
                PasswordHash = "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9",
                Role = "admin"
            },
            new User
            {
                Id = 2,
                Username = "teacher1",
                Email = "teacher@example.com",
                PasswordHash = "63cb9c6fa2d65784658539a93ad47f2274a02ddff344537beb97bd399938ad22",
                Role = "teacher"
            },
            new User
            {
                Id = 3,
                Username = "student1",
                Email = "student@example.com",
                PasswordHash = "19b9dd3e24fad97f47400340f81e118ca3f88be2ee3503b34b9bde0ad5ad7ebd",
                Role = "student"
            },
            new User
            {
                Id = 4,
                Username = "guest1",
                Email = "guest@example.com",
                PasswordHash = "6b93ccba414ac1d0ae1e77f3fac560c748a6701ed6946735a49d463351518e16",
                Role = "guest"
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
