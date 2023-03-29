using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Domain.Common;
using CyberWork.Accounting.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CyberWork.Accounting.Infrastructure.Common;

public class RepositoryBase<T, K> : RepositoryQueryBase<T, K> where T : BaseEntity<K>
{
}

public class RepositoryBase<T, K, TContext> : RepositoryQueryBase<T, K, TContext>,
    IRepositoryBase<T, K, TContext>
    where T : BaseEntity<K>
    where TContext : DbContext
{
    private readonly TContext _dbContext;
    private readonly IUnitOfWork<TContext> _unitOfWork;

    public RepositoryBase(TContext dbContext, IUnitOfWork<TContext> unitOfWork) : base(dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public TContext GetContext() => _dbContext;

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken) =>
        _dbContext.Database.BeginTransactionAsync(cancellationToken);

    public async Task EndTransactionAsync(CancellationToken cancellationToken)
    {
        await SaveChangesAsync(cancellationToken);
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken) =>
        _dbContext.Database.RollbackTransactionAsync(cancellationToken);

    public void Create(T entity) => _dbContext.Set<T>().Add(entity);

    public async Task<K> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public IList<K> CreateList(IEnumerable<T> entities)
    {
        _dbContext.Set<T>().AddRange(entities);
        return entities.Select(x => x.Id).ToList();
    }

    public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities,
        CancellationToken cancellationToken)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities);
        await SaveChangesAsync(cancellationToken);
        return entities.Select(x => x.Id).ToList();
    }

    public void Update(T entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Unchanged) return;

        T exist = _dbContext.Set<T>().Find(entity.Id);
        _dbContext.Entry(exist).CurrentValues.SetValues(entity);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        if (_dbContext.Entry(entity).State == EntityState.Unchanged) return;

        T exist = _dbContext.Set<T>().Find(entity.Id);
        _dbContext.Entry(exist).CurrentValues.SetValues(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public void UpdateList(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
            Update(entity);
    }

    public async Task UpdateListAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
            Update(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public void Delete(T entity) =>
        _dbContext.Set<T>().Remove(entity);

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<T>().Remove(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public void DeleteList(IEnumerable<T> entities) => _dbContext.Set<T>().RemoveRange(entities);

    public async Task DeleteListAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        _dbContext.Set<T>().RemoveRange(entities);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        await _unitOfWork.CommitAsync(cancellationToken);
}