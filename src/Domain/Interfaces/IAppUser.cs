namespace CyberWork.Accounting.Domain.Interfaces;

public interface IAppUser
{
    Guid Id { get; set; }
    string UserName { get; set; }
    string Email { get; set; }
    string PhoneNumber { get; set; }
    Status Status { get; set; }
    UserProfile UserProfile { get; set; }
}