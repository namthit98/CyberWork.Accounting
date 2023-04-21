using CyberWork.Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CyberWork.Accounting.Infrastructure.Persistence.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder
            .HasKey(up =>
                new { up.RoleId, up.AppResourceActionId, up.AppResourceId });

        builder
            .HasOne(arc => arc.AppResourceAction)
            .WithMany(up => up.RolePermissions)
            .HasForeignKey(arc => arc.AppResourceActionId);

        builder
            .HasOne(arc => arc.AppResource)
            .WithMany(up => up.RolePermissions)
            .HasForeignKey(arc => arc.AppResourceId);
    }
}