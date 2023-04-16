using CyberWork.Accounting.Application.Common.Mappings;
using CyberWork.Accounting.Domain.Entities;
using CyberWork.Accounting.Domain.Enums;

namespace CyberWork.Accounting.Application.Users.DTOs;

public class UserProfileDto : IMapFrom<UserProfile>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PersonalEmail { get; init; }
    public DateTime Birthday { get; init; }
    public Gender Gender { get; init; }
    public string Address { get; init; }
}