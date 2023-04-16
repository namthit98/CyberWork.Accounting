using CyberWork.Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Organization> Organizations { get; }
    DbSet<UserProfile> UserProfiles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}