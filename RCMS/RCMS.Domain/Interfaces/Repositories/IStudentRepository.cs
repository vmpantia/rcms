using RCMS.Domain.Entities;
using RCMS.Shared.Models.Students;

namespace RCMS.Domain.Interfaces.Repositories;

public interface IStudentRepository : IBaseRepository<Student>
{
    Task<IEnumerable<Student>> FilterAsync(FilterStudent filter, CancellationToken cancellationToken = default);
    Task DeleteAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}