using Microsoft.EntityFrameworkCore;
using RCMS.Domain.Entities;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Infrastructure.DataAccess.Contexts;
using RCMS.Shared.Enumerations;
using RCMS.Shared.Models.Courses;

namespace RCMS.Infrastructure.DataAccess.Repositories;

public sealed class CourseCategoryRepository(RCMSDbContext context) : BaseRepository<CourseCategory>(context), ICourseCategoryRepository
{
    public async Task<IEnumerable<CourseCategory>> FilterAsync(FilterCourseCategory filter, CancellationToken cancellationToken = default)
    {
        // Get initial data stored on the database
        var data = Get();
        
        // Filter by category name
        if (!string.IsNullOrWhiteSpace(filter?.Name))
            data = data.Where(cc => cc.Name.Contains(filter.Name));
        
        // Filter by statuses
        if (filter?.Statuses != null && filter.Statuses.Any())
        {
            var statuses = filter.Statuses
                .Select(s => Enum.TryParse<CourseCategoryStatus>(s, out var status) ? status : default)
                .ToList();
            data = data.Where(cc => statuses.Contains(cc.Status));
        }
        
        return await data
            .OrderByDescending(cc => cc.CreatedAt)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}