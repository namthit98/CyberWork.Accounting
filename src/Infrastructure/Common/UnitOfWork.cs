using CyberWork.Accounting.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Infrastructure.Common;


public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
{
    private readonly TContext _context;

    public UnitOfWork(TContext context)
    {
        _context = context;
    }

    public void Dispose() => _context.Dispose();

    public Task<int> CommitAsync(CancellationToken cancellationToken) =>
        _context.SaveChangesAsync(cancellationToken);
}