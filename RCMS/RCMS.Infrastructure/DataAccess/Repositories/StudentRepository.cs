using Microsoft.EntityFrameworkCore;
using RCMS.Domain.Entities;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Infrastructure.DataAccess.Contexts;

namespace RCMS.Infrastructure.DataAccess.Repositories;

public sealed class StudentRepository(RCMSDbContext context) : BaseRepository<Student>(context), IStudentRepository
{
    public async Task DeleteAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        await context.Students
            .Where(s => ids.Contains(s.Id))
            .ExecuteDeleteAsync(cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }
}