using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<Result<Guid>>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    public string Email { get; init; }
}