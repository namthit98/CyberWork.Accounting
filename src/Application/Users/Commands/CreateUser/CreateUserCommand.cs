using AutoMapper;
using CyberWork.Accounting.Application.Common.Mappings;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Domain.Entities;
using CyberWork.Accounting.Domain.Enums;
using CyberWork.Accounting.Domain.Interfaces;
using MediatR;

namespace CyberWork.Accounting.Application.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<Result<Guid>>,
    IMapFrom<IAppUser>
{
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PersonalEmail { get; init; }
    public DateTime Birthday { get; init; }
    public Gender Gender { get; init; }
    public string Address { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserCommand, UserProfile>();
    }
}