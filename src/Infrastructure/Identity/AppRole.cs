using CyberWork.Accounting.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CyberWork.Accounting.Domain.Entities;

public class AppRole : IdentityRole<Guid>, IAppRole
{
    public string Description { get; set; }
}