using System.Linq.Expressions;
using CyberWork.Accounting.Domain.Common;
using CyberWork.Accounting.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Infrastructure.Common;

public class RepositoryQueryBase<T, K>
    where T : BaseEntity<K>
{

}

public class RepositoryQueryBase<T, K, TContext> : RepositoryQueryBase<T, K>,
    IRepositoryQueryBase<T, K, TContext>
        where T : BaseEntity<K>
        where TContext : DbContext
{
    private readonly TContext _dbContext;

    public RepositoryQueryBase(TContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IQueryable<T> FindAll(bool trackChanges = false) =>
        !trackChanges ? _dbContext.Set<T>().AsNoTracking() :
            _dbContext.Set<T>();

    public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindAll(trackChanges);
        items = includeProperties.Aggregate(items, (current, includeProperty) =>
            current.Include(includeProperty));
        return items;
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
        !trackChanges
            ? _dbContext.Set<T>().Where(expression).AsNoTracking()
            : _dbContext.Set<T>().Where(expression);

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindByCondition(expression, trackChanges);
        items = includeProperties.Aggregate(items, (current, includeProperty) =>
            current.Include(includeProperty));
        return items;
    }

    public async Task<T> GetByIdAsync(K id) =>
        await FindByCondition(x => x.Id.Equals(id))
            .FirstOrDefaultAsync();

    public async Task<T> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties) =>
        await FindByCondition(x => x.Id.Equals(id), trackChanges: false, includeProperties)
            .FirstOrDefaultAsync();
}