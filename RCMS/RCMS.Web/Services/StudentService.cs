using MudBlazor;
using RCMS.Shared.Models.Students;
using RCMS.Web.Providers.Contracts;
using RCMS.Web.Services.Contracts;

namespace RCMS.Web.Services;

public class StudentService(IHttpClientProvider httpClientProvider, ILogger<AuthService> logger) : IStudentService
{
    public async Task<IEnumerable<StudentLiteDto>> GetStudentsAsync()
    {
        try
        {
            // Send getting of student request to API
            var students = await httpClientProvider.GetAsync<IEnumerable<StudentLiteDto>>("https://localhost:7226/api/Students");
            return students;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in getting students. | {ex.Message}");
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
}