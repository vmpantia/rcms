using RCMS.Infrastructure.DataAccess.Entities.Contracts;
using RCMS.Shared.Enumerations;

namespace RCMS.Infrastructure.DataAccess.Entities;

public class Enrollment : BaseEntity
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public EnrollmentStatus Status { get; set; }
    
    public virtual Student Student { get; set; }
    public virtual Course Course { get; set; }
}