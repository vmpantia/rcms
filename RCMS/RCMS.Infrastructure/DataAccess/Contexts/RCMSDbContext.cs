using Microsoft.EntityFrameworkCore;
using RCMS.Domain.Entities;
using RCMS.Infrastructure.Extensions;

namespace RCMS.Infrastructure.DataAccess.Contexts;

public class RCMSDbContext : DbContext
{
    public RCMSDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseCategory> CourseCategories { get; set; }
    public DbSet<CourseSession> CourseSessions { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(builder =>
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Category)
                .WithMany(cc => cc.Courses)
                .HasForeignKey(c => c.CategoryId)
                .IsRequired();
            
            builder.HasQueryFilter(cc => cc.DeletedAt == null && string.IsNullOrEmpty(cc.DeletedBy));
        });
        
        modelBuilder.Entity<CourseCategory>(builder =>
        {
            builder.HasKey(cc => cc.Id);
            
            builder.HasQueryFilter(cc => cc.DeletedAt == null && string.IsNullOrEmpty(cc.DeletedBy));
        });
        
        modelBuilder.Entity<CourseSession>(builder =>
        {
            builder.HasKey(c => c.Id);
            
            builder.HasOne(cs => cs.Course)
                .WithMany(c => c.Sessions)
                .HasForeignKey(cs => cs.CourseId)
                .IsRequired();
            
            builder.HasOne(cs => cs.Instructor)
                .WithMany(i => i.Sessions)
                .HasForeignKey(cs => cs.InstructorId)
                .IsRequired();
            
            builder.HasQueryFilter(cs => cs.DeletedAt == null && string.IsNullOrEmpty(cs.DeletedBy));
        });

        modelBuilder.Entity<Enrollment>(builder =>
        {
            builder.HasKey(e => e.Id);
            
            builder.HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();
            
            builder.HasOne(e => e.Session)
                .WithMany(cs => cs.Enrollments)
                .HasForeignKey(e => e.SessionId)
                .IsRequired();
            
            builder.HasQueryFilter(e => e.DeletedAt == null && string.IsNullOrEmpty(e.DeletedBy));
        });
        
        modelBuilder.Entity<Instructor>(builder =>
        {
            builder.HasKey(i => i.Id);
            
            builder.HasQueryFilter(i => i.DeletedAt == null && string.IsNullOrEmpty(i.DeletedBy));
        });
        
        modelBuilder.Entity<Schedule>(builder =>
        {
            builder.HasKey(s => s.Id);
            
            builder.HasOne(s => s.Session)
                .WithMany(cs => cs.Schedules)
                .HasForeignKey(s => s.SessionId)
                .IsRequired();
            
            builder.HasQueryFilter(s => s.DeletedAt == null && string.IsNullOrEmpty(s.DeletedBy));
        });
        
        modelBuilder.Entity<Student>(builder =>
        {
            builder.HasKey(s => s.Id);
            
            builder.HasQueryFilter(s => s.DeletedAt == null && string.IsNullOrEmpty(s.DeletedBy));
        });

        modelBuilder.Entity<User>(builder =>
        {
            builder.HasKey(u => u.Id);
            
            builder.HasQueryFilter(u => u.DeletedAt == null && string.IsNullOrEmpty(u.DeletedBy));
        });
    }
}