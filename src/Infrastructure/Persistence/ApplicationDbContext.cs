using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CyberWork.Accounting.Infrastructure.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using CyberWork.Accounting.Domain.Entities;
using CyberWork.Accounting.Infrastructure.Persistence.Interceptors;
using CyberWork.Accounting.Application.Common.Interfaces;

namespace CyberWork.Accounting.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator) : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Organization> Organizations => Set<Organization>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}