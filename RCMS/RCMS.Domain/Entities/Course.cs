using RCMS.Shared.Enumerations;

namespace RCMS.Domain.Entities;

public class Course : BaseEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int DurationInHours { get; set; }
    public CourseStatus Status { get; set; }
    
    public virtual ICollection<Enrollment> Enrollments { get; set; }
    public virtual ICollection<Schedule> Schedules { get; set; }
}