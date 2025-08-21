using System.Linq.Expressions;
using RCMS.Infrastructure.DataAccess.Entities.Contracts;

namespace RCMS.Infrastructure.DataAccess.Repositories.Contracts;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> Get();
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression);
    Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}