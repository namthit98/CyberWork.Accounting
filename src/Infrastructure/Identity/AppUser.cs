using CyberWork.Accounting.Domain.Enums;
using CyberWork.Accounting.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CyberWork.Accounting.Domain.Entities;

public class AppUser : IdentityUser<Guid>, IAppUser
{
    public Status Status { get; set; } = Status.Active;
    public Guid UserProfileId { get; set; }
    public virtual UserProfile UserProfile { get; set; }
}