using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Domain.Enums;
using MediatR;

namespace CyberWork.Accounting.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; private set; }
    public void SetId(Guid id)
    {
        Id = id;
    }

    public string PhoneNumber { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PersonalEmail { get; init; }
    public DateTime Birthday { get; init; }
    public Gender Gender { get; init; }
    public string Address { get; init; }
}