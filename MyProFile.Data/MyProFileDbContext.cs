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
    public DbSet<Mentor> Mentors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Fluent конфигурации 
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

    }
}
