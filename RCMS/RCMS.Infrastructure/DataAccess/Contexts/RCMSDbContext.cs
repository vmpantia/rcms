using Microsoft.EntityFrameworkCore;
using RCMS.Infrastructure.DataAccess.Entities;
using RCMS.Infrastructure.Extensions;

namespace RCMS.Infrastructure.DataAccess.Contexts;

public class RCMSDbContext : DbContext
{
    public RCMSDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyBaseEntityFilters();

        modelBuilder.Entity<Course>(builder =>
        {
            builder.HasKey(c => c.Id);
        });

        modelBuilder.Entity<Enrollment>(builder =>
        {
            builder.HasKey(e => e.Id);
            
            builder.HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();
            
            builder.HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .IsRequired();
        });
        
        modelBuilder.Entity<Instructor>(builder =>
        {
            builder.HasKey(i => i.Id);
        });
        
        modelBuilder.Entity<Schedule>(builder =>
        {
            builder.HasKey(s => s.Id);
            
            builder.HasOne(s => s.Course)
                .WithMany(c => c.Schedules)
                .HasForeignKey(s => s.CourseId)
                .IsRequired();
        });
        
        modelBuilder.Entity<Student>(builder =>
        {
            builder.HasKey(s => s.Id);
        });
    }
}