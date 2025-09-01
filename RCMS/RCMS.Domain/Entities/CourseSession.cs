using RCMS.Shared.Enumerations;

namespace RCMS.Domain.Entities;

public class CourseSession : BaseEntity
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid InstructorId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public CourseSessionStatus Status { get; set; }
    
    public virtual Course Course { get; set; }
    public virtual Instructor Instructor { get; set; }
    
    public virtual ICollection<Enrollment> Enrollments { get; set; }
    public virtual ICollection<Schedule> Schedules { get; set; }
}