using RCMS.Shared.Models.Instructors;
using RCMS.Web.Interfaces.Providers;
using RCMS.Web.Interfaces.Services;

namespace RCMS.Web.Services;

public class InstructorService(IHttpClientProvider httpClientProvider, ILogger<AuthService> logger) : IInstructorService
{
    public async Task<IEnumerable<InstructorLiteDto>> GetInstructorsAsync()
    {
        try
        {
            // Send getting of instructors request to API
            var instructors = await httpClientProvider.GetAsync<IEnumerable<InstructorLiteDto>>("https://localhost:7226/api/Instructors");
            return instructors;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in getting instructors. | {ex.Message}");
            throw;
        }
    }
    
    public async Task<InstructorDto> GetInstructorByIdAsync(Guid id)
    {
        try
        {
            // Send getting of instructor request to API
            var instructor = await httpClientProvider.GetAsync<InstructorDto>($"https://localhost:7226/api/Instructors/{id}");
            return instructor;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in getting instructor. | {ex.Message}");
            throw;
        }
    }
    
    public async Task CreateInstructorAsync(CreateInstructorDto request)
    {
        try
        {
            // Send creating of instructor request to API
            await httpClientProvider.PostAsync<Guid>("https://localhost:7226/api/Instructors", request);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in creating instructor. | {ex.Message}");
            throw;
        }
    }
    
    public async Task UpdateInstructorAsync(Guid id, UpdateInstructorDto request)
    {
        try
        {
            // Send updating of instructor request to API
            await httpClientProvider.PutAsync<Guid>($"https://localhost:7226/api/Instructors/{id}", request);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in updating instructor. | {ex.Message}");
            throw;
        }
    }
    
    public async Task DeleteInstructorAsync(Guid id)
    {
        try
        {
            // Send delete of instructor request to API
            await httpClientProvider.DeleteAsync<Guid>($"https://localhost:7226/api/Instructors/{id}");
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in deleting instructor. | {ex.Message}");
            throw;
        }
    }
    
    public async Task DeleteInstructorsAsync(DeleteInstructorsDto request)
    {
        try
        {
            // Send delete of instructor request to API
            await httpClientProvider.DeleteAsync<IEnumerable<Guid>>($"https://localhost:7226/api/Instructors", request);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in deleting instructors. | {ex.Message}");
            throw;
        }
    }
}