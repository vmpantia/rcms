using RCMS.Shared.Models.Students;

namespace RCMS.Web.Interfaces.Services;

public interface IStudentService
{
    Task<IEnumerable<StudentLiteDto>> GetStudentsAsync(FilterStudent filter);
    Task<StudentDto> GetStudentByIdAsync(Guid id);
    Task CreateStudentAsync(SaveStudentDto request);
    Task UpdateStudentAsync(Guid id, SaveStudentDto request);
    Task DeleteStudentAsync(Guid id);
    Task DeleteStudentsAsync(DeleteStudentsDto request);
}