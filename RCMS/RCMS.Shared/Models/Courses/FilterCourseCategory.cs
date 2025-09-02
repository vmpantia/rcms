namespace RCMS.Shared.Models.Courses;

public class FilterCourseCategory
{
    public string? Name { get; set; }
    public IEnumerable<string>? Statuses { get; set; }
}