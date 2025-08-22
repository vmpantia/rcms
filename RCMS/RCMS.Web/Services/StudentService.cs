using MudBlazor;
using RCMS.Shared.Models.Students;
using RCMS.Web.Providers.Contracts;
using RCMS.Web.Services.Contracts;

namespace RCMS.Web.Services;

public class StudentService(IHttpClientProvider httpClientProvider, ISnackbar snackbar, ILogger<AuthService> logger) : 
    BaseService<AuthService>(snackbar, logger), IStudentService
{
    public async Task<IEnumerable<StudentLiteDto>> GetStudentsAsync()
    {
        try
        {
            // Send login request to API
            var students = await httpClientProvider.GetAsync<IEnumerable<StudentLiteDto>>("https://localhost:7226/api/Students");
            return students;
        }
        catch (Exception ex)
        {
            HandleUnexpectedError(ex, "Error in getting students.");
            return [];
        }
    }
}