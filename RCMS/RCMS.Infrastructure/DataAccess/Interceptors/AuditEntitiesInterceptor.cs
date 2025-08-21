using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RCMS.Infrastructure.DataAccess.Entities.Contracts;

namespace RCMS.Infrastructure.DataAccess.Interceptors;

public class AuditEntitiesInterceptor(IHttpContextAccessor httpContextAccessor) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        // Get database context
        var dbContext = eventData.Context;
        
        // Do nothing when database context is NULL
        if(dbContext is null) return base.SavingChangesAsync(eventData, result, cancellationToken);

        // Get all the entries for base entity
        var entries = dbContext.ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            // Get entity from the entry
            var entity = entry.Entity;
            
            // Get requestor info if not exist use default
            var requestor = httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "System";

            switch (entry.State)
            {
                case EntityState.Added:
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.CreatedBy = requestor;
                    break;
                case EntityState.Modified:
                    entity.ModifiedAt = DateTime.UtcNow;
                    entity.ModifiedBy = requestor;
                    break;
                case EntityState.Deleted: // Use soft delete
                    entry.State = EntityState.Modified;
                    entity.DeletedAt = DateTime.UtcNow;
                    entity.DeletedBy = requestor;
                    break;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}