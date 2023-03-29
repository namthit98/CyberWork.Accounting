using CyberWork.Accounting.Application.Common.Exceptions;
using CyberWork.Accounting.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CyberWork.Accounting.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<AppUser> _userManager;

    public IdentityService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Guid> CreateUserAsync(string firstName, string lastName,
        string userName, string password, string email)
    {
        var user = new AppUser
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = userName,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Errors.Any())
        {
            var failure = result.Errors.Select(x => x.Description).FirstOrDefault();

            if (!String.IsNullOrEmpty(failure))
                throw new ConflictException(failure);
        }

        return user.Id;
    }
}