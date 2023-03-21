using CyberWork.Accounting.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CyberWork.Accounting.Infrastructure.Interfaces;

public interface IRepositoryBase<T, K> : IRepositoryQueryBase<T, K>
    where T : BaseEntity<K>
{
    void Create(T entity);
    Task<K> CreateAsync(T entity, CancellationToken cancellationToken);
    IList<K> CreateList(IEnumerable<T> entities);
    Task<IList<K>> CreateListAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
    void Update(T entity);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    void UpdateList(IEnumerable<T> entities);
    Task UpdateListAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
    void Delete(T entity);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
    void DeleteList(IEnumerable<T> entities);
    Task DeleteListAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    Task EndTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
}

public interface IRepositoryBase<T, K, TContext> : IRepositoryBase<T, K>
    where T : BaseEntity<K>
    where TContext : DbContext
{
}