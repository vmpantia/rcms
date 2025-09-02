using RCMS.Shared.Enumerations;

namespace RCMS.Shared.Models.Courses;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public CourseStatus Status { get; set; }  
    public CourseCategoryDto Category { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public string LastModifiedBy { get; set; }
}