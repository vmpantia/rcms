using RCMS.Domain.Entities;

namespace RCMS.Domain.Interfaces.Repositories;

public interface IInstructorRepository : IBaseRepository<Instructor>
{
    Task DeleteAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}