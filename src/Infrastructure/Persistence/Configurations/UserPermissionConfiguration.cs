using CyberWork.Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CyberWork.Accounting.Infrastructure.Persistence.Configurations;

public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder
            .HasKey(up =>
                new { up.UserId, up.AppResourceActionId, up.AppResourceId });

        builder
            .HasOne(arc => arc.AppResourceAction)
            .WithMany(up => up.UserPermissions)
            .HasForeignKey(arc => arc.AppResourceActionId);

        builder
            .HasOne(arc => arc.AppResource)
            .WithMany(up => up.UserPermissions)
            .HasForeignKey(arc => arc.AppResourceId);
    }
}