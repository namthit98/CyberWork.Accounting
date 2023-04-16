using CyberWork.Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CyberWork.Accounting.Infrastructure.Persistence.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder
            .HasOne(up => up.UserProfile)
            .WithOne()
            .HasForeignKey<AppUser>(u => u.UserProfileId);
    }
}