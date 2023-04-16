using CyberWork.Accounting.Application.Common.Mappings;
using CyberWork.Accounting.Domain.Enums;
using CyberWork.Accounting.Domain.Interfaces;

namespace CyberWork.Accounting.Application.Users.DTOs;

public class UserDto : IMapFrom<IAppUser>
{
    public Guid Id { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public Status Status { get; init; }
    public virtual UserProfileDto UserProfile { get; set; }
}