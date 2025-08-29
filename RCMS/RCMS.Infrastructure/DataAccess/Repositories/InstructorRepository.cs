using Microsoft.EntityFrameworkCore;
using RCMS.Domain.Entities;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Infrastructure.DataAccess.Contexts;

namespace RCMS.Infrastructure.DataAccess.Repositories;

public sealed class InstructorRepository(RCMSDbContext context) : BaseRepository<Instructor>(context), IInstructorRepository
{
    public async Task DeleteAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var dataToDelete = await context.Instructors
            .Where(s => ids.Contains(s.Id))
            .ToListAsync(cancellationToken);
        
        context.Instructors.RemoveRange(dataToDelete);
        
        await context.SaveChangesAsync(cancellationToken);
    }
}