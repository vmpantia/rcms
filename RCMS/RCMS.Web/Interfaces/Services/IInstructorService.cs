using RCMS.Shared.Models.Instructors;

namespace RCMS.Web.Interfaces.Services;

public interface IInstructorService
{
    Task<IEnumerable<InstructorLiteDto>> GetInstructorsAsync();
    Task<InstructorDto> GetInstructorByIdAsync(Guid id);
    Task CreateInstructorAsync(SaveInstructorDto request);
    Task UpdateInstructorAsync(Guid id, SaveInstructorDto request);
    Task DeleteInstructorAsync(Guid id);
    Task DeleteInstructorsAsync(DeleteInstructorsDto request);
}