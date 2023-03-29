namespace CyberWork.Accounting.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Guid> CreateUserAsync(string firstName, string lastName,
        string userName, string password, string email);

}