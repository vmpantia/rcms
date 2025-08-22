using RCMS.Shared.Models.Students;

namespace RCMS.Web.Services.Contracts;

public interface IStudentService
{
    Task<IEnumerable<StudentLiteDto>> GetStudentsAsync();
}