using RCMS.Domain.Entities;

namespace RCMS.Domain.Interfaces.Repositories;

public interface IStudentRepository : IBaseRepository<Student>
{
    Task DeleteAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}