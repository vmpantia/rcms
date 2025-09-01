using RCMS.Shared.Enumerations;

namespace RCMS.Domain.Entities;

public class Course : BaseEntity
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CourseStatus Status { get; set; }  
    
    public virtual CourseCategory Category { get; set; }
    public virtual ICollection<CourseSession> Sessions { get; set; }
}