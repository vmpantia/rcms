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
    
    public async Task CreateCourseAsync(SaveCourseDto request)
    {
        try
        {
            // Send creating of course request to API
            await httpClientProvider.PostAsync<Guid>("https://localhost:7226/api/Courses", request);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in creating course. | {ex.Message}");
            throw;
        }
    }

    public async Task UpdateCourseAsync(Guid courseId, SaveCourseDto request)
    {
        try
        {
            // Send updating of course request to API
            await httpClientProvider.PutAsync<Guid>("https://localhost:7226/api/Courses", request);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in updating course. | {ex.Message}");
            throw;
        }
    }
}