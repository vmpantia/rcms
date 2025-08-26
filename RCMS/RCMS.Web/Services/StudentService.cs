using RCMS.Shared.Models.Students;
using RCMS.Web.Interfaces.Providers;
using RCMS.Web.Interfaces.Services;

namespace RCMS.Web.Services;

public class StudentService(IHttpClientProvider httpClientProvider, ILogger<AuthService> logger) : IStudentService
{
    public async Task<IEnumerable<StudentLiteDto>> GetStudentsAsync()
    {
        try
        {
            // Send getting of students request to API
            var students = await httpClientProvider.GetAsync<IEnumerable<StudentLiteDto>>("https://localhost:7226/api/Students");
            return students;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in getting students. | {ex.Message}");
            throw;
        }
    }
    
    public async Task<StudentDto> GetStudentByIdAsync(Guid id)
    {
        try
        {
            // Send getting of student request to API
            var student = await httpClientProvider.GetAsync<StudentDto>($"https://localhost:7226/api/Students/{id}");
            return student;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in getting student. | {ex.Message}");
            throw;
        }
    }
    
    public async Task CreateStudentAsync(CreateStudentDto request)
    {
        try
        {
            // Send creating of student request to API
            await httpClientProvider.PostAsync<Guid>("https://localhost:7226/api/Students", request);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in creating student. | {ex.Message}");
            throw;
        }
    }
    
    public async Task UpdateStudentAsync(Guid id, UpdateStudentDto request)
    {
        try
        {
            // Send updating of student request to API
            await httpClientProvider.PutAsync<Guid>($"https://localhost:7226/api/Students/{id}", request);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in updating student. | {ex.Message}");
            throw;
        }
    }
    
    public async Task DeleteStudentAsync(Guid id)
    {
        try
        {
            // Send delete of student request to API
            await httpClientProvider.DeleteAsync<Guid>($"https://localhost:7226/api/Students/{id}");
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in deleting student. | {ex.Message}");
            throw;
        }
    }
    
    public async Task DeleteStudentsAsync(DeleteStudentsDto request)
    {
        try
        {
            // Send delete of student request to API
            await httpClientProvider.DeleteAsync<IEnumerable<Guid>>($"https://localhost:7226/api/Students", request);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in deleting students. | {ex.Message}");
            throw;
        }
    }
}