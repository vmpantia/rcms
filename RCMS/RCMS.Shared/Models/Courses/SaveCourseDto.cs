namespace RCMS.Shared.Models.Courses;

public class SaveCourseDto
{
    public string Name { get; set; }
    public string CategoryId { get; set; }
    public string? Description { get; set; }
}