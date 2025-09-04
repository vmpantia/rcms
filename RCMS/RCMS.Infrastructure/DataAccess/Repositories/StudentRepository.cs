using Microsoft.EntityFrameworkCore;
using RCMS.Domain.Entities;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Infrastructure.DataAccess.Contexts;
using RCMS.Shared.Enumerations;
using RCMS.Shared.Models.Students;

namespace RCMS.Infrastructure.DataAccess.Repositories;

public sealed class StudentRepository(RCMSDbContext context) : BaseRepository<Student>(context), IStudentRepository
{
    public async Task<IEnumerable<Student>> FilterAsync(FilterStudent filter, CancellationToken cancellationToken = default)
    {
        // Get initial data stored on the database
        var data = Get();
        
        // Filter by first name
        if (!string.IsNullOrWhiteSpace(filter?.FirstName))
            data = data.Where(s => s.FirstName.Contains(filter.FirstName));
        
        // Filter by last name
        if (!string.IsNullOrWhiteSpace(filter?.LastName))
            data = data.Where(s => s.LastName.Contains(filter.LastName));
        
        // Filter by phone number
        if (!string.IsNullOrWhiteSpace(filter?.PhoneNumber))
            data = data.Where(s => s.PhoneNumber.Contains(filter.PhoneNumber));
        
        // Filter by email address
        if (!string.IsNullOrWhiteSpace(filter?.EmailAddress))
            data = data.Where(s => s.EmailAddress.Contains(filter.EmailAddress));
        
        // Filter by genders
        if (filter?.Genders != null && filter.Genders.Any())
        {
            var genders = filter.Genders
                .Select(s => Enum.TryParse<GenderType>(s, out var gender) ? gender : default)
                .ToList();
            data = data.Where(s => genders.Contains(s.Gender));
        }
        
        // Filter by statuses
        if (filter?.Statuses != null && filter.Statuses.Any())
        {
            var statuses = filter.Statuses
                .Select(s => Enum.TryParse<StudentStatus>(s, out var status) ? status : default)
                .ToList();
            data = data.Where(s => statuses.Contains(s.Status));
        }
        
        return await data
            .OrderByDescending(tbl => tbl.CreatedAt)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {        
        var dataToDelete = await context.Students
            .Where(s => ids.Contains(s.Id))
            .ToListAsync(cancellationToken);
        
        context.Students.RemoveRange(dataToDelete);
        
        await context.SaveChangesAsync(cancellationToken);
    }
}