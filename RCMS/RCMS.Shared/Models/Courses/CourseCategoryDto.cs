using RCMS.Shared.Enumerations;

namespace RCMS.Shared.Models.Courses;

public class CourseCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int NoOfCourses { get; set; }
    public CourseCategoryStatus Status { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public string LastModifiedBy { get; set; }
}