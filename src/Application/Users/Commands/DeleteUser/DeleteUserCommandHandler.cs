using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommand, Result<Guid>>
{
    private readonly IUserServices _userServices;

    public DeleteUserCommandHandler(IUserServices userServices)
    {
        _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
    }

    public async Task<Result<Guid>> Handle(DeleteUserCommand role,
        CancellationToken cancellationToken)
    {
        var result = await _userServices.DeleteUserAsync(role.Id);

        return Result<Guid>.Success(result);
    }
}