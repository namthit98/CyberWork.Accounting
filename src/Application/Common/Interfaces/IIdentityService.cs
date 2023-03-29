using CyberWork.Accounting.Application.Common.Models;

namespace CyberWork.Accounting.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Guid> CreateUserAsync(string firstName, string lastName,
        string userName, string password, string email);
    //Task<Guid> UpdateUserAsync(string userId, string firstName, string lastName);
    //Task<Guid> DeleteUserAsync(string userId);
    //Task<Guid> ChangePasswordAsync(string userId);

}