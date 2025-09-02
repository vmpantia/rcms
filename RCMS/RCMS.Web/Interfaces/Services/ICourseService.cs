using RCMS.Shared.Models.Courses;

namespace RCMS.Web.Interfaces.Services;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetCoursesAsync();
    Task<IEnumerable<CourseCategoryDto>> GetCourseCategoriesAsync();
}