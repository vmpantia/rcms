using RCMS.Shared.Models.Courses;

namespace RCMS.Web.Interfaces.Services;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetCoursesAsync();
    Task<IEnumerable<CourseCategoryDto>> GetCourseCategoriesAsync(FilterCourseCategory filter);
    Task CreateCourseAsync(SaveCourseDto request);
    Task UpdateCourseAsync(Guid courseId, SaveCourseDto request);
}