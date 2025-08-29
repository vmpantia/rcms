using RCMS.Shared.Models.Instructors;

namespace RCMS.Web.Interfaces.Services;

public interface IInstructorService
{
    Task<IEnumerable<InstructorLiteDto>> GetInstructorsAsync();
    Task<InstructorDto> GetInstructorByIdAsync(Guid id);
    Task CreateInstructorAsync(CreateInstructorDto request);
    Task UpdateInstructorAsync(Guid id, UpdateInstructorDto request);
    Task DeleteInstructorAsync(Guid id);
    Task DeleteInstructorsAsync(DeleteInstructorsDto request);
}