using RCMS.Shared.Models.Students;

namespace RCMS.Web.Interfaces.Services;

public interface IStudentService
{
    Task<IEnumerable<StudentLiteDto>> GetStudentsAsync();
    Task<StudentDto> GetStudentByIdAsync(Guid id);
    Task CreateStudentAsync(CreateStudentDto request);
    Task UpdateStudentAsync(Guid id, UpdateStudentDto request);
    Task DeleteStudentAsync(Guid id);
}