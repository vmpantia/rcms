using RCMS.Shared.Enumerations;

namespace RCMS.Domain.Entities;

public class CourseCategory : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public CourseCategoryStatus Status { get; set; }
    
    public virtual ICollection<Course> Courses { get; set; }
}