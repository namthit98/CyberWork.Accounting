using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IIdentityService _identityService;

    public CreateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    public async Task<Result<Guid>> Handle(CreateUserCommand user,
       CancellationToken cancellationToken)
    {
        var result = await _identityService.CreateUserAsync(
            user.FirstName, user.LastName, user.Username, user.Password, user.Email);

        return Result<Guid>.Success(result);
    }
}