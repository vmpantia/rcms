using RCMS.Shared.Enumerations;

namespace RCMS.Domain.Entities;

public class Enrollment : BaseEntity
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid SessionId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public EnrollmentStatus Status { get; set; }
    
    public virtual Student Student { get; set; }
    public virtual CourseSession Session { get; set; }
}