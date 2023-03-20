using Microsoft.AspNetCore.Identity;

namespace CyberWork.Accounting.Infrastructure.Identity;
public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}