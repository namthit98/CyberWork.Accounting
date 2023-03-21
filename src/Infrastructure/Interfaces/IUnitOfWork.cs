using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync(CancellationToken cancellationToken);
}


public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
}