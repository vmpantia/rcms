namespace RCMS.Shared.Models.Courses;

public class SaveCourseDto
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}