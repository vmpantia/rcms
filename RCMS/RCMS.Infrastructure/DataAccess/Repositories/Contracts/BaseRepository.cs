using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RCMS.Infrastructure.DataAccess.Contexts;
using RCMS.Infrastructure.DataAccess.Entities.Contracts;

namespace RCMS.Infrastructure.DataAccess.Repositories.Contracts;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly RCMSDbContext _context;
    private readonly DbSet<TEntity> _table;

    protected BaseRepository(RCMSDbContext context)
    {
        _context = context;
        _table = context.Set<TEntity>();
    }
    
    public IQueryable<TEntity> Get()
    {
        return _table;
    }

    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression)
    {
        return _table.Where(expression);
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _table.AnyAsync(expression, cancellationToken);
    }

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var result = await _table.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return result.Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var result = _table.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        
        return result.Entity;
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _table.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}