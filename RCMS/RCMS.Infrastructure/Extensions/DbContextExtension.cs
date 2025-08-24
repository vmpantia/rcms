using Microsoft.EntityFrameworkCore;
using RCMS.Domain.Entities;

namespace RCMS.Infrastructure.Extensions;

internal static class DbContextExtension
{
    internal static void ApplyBaseEntityFilters(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;
            if (!typeof(BaseEntity).IsAssignableFrom(clrType)) continue;

            var method = typeof(DbContextExtension)
                .GetMethod(nameof(ApplyBaseEntityFilter))?
                .MakeGenericMethod(clrType);

            method?.Invoke(null, new object[] { modelBuilder });
        }
    }

    private static void ApplyBaseEntityFilter<TEntity>(ModelBuilder builder) where TEntity : BaseEntity
    {
        builder.Entity<TEntity>().HasQueryFilter(e => e.DeletedAt == null && string.IsNullOrEmpty(e.DeletedBy));
    }
}