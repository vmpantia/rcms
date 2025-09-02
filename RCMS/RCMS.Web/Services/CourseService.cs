using RCMS.Shared.Models.Courses;
using RCMS.Web.Interfaces.Providers;
using RCMS.Web.Interfaces.Services;

namespace RCMS.Web.Services;

public class CourseService(IHttpClientProvider httpClientProvider, ILogger<CourseService> logger) : ICourseService
{
    public async Task<IEnumerable<CourseDto>> GetCoursesAsync()
    {
        try
        {
            // Send getting of courses request to API
            var courses = await httpClientProvider.GetAsync<IEnumerable<CourseDto>>("https://localhost:7226/api/Courses");
            return courses;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in getting courses. | {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<CourseCategoryDto>> GetCourseCategoriesAsync()
    {
        try
        {
            // Send getting of course categories request to API
            var categories = await httpClientProvider.GetAsync<IEnumerable<CourseCategoryDto>>("https://localhost:7226/api/Courses/Categories");
            return categories;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in getting course categories. | {ex.Message}");
            throw;
        }
    }
}