using System.Linq.Expressions;
using CyberWork.Accounting.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Infrastructure.Interfaces;

public interface IRepositoryQueryBase<T, in K>
    where T : BaseEntity<K>
{
    IQueryable<T> FindAll(bool trackChanges = false);
    IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false,
        params Expression<Func<T, object>>[] includeProperties);

    Task<T> GetByIdAsync(K id);
    Task<T> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties);
}

public interface IRepositoryQueryBase<T, K, TContext> : IRepositoryQueryBase<T, K> where T : BaseEntity<K>
    where TContext : DbContext
{
}
