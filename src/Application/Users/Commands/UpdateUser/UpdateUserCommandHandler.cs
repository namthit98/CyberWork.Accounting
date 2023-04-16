using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, Result<Guid>>
{
    private readonly IUserServices _userServices;

    public UpdateUserCommandHandler(IUserServices userServices)
    {
        _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
    }

    public async Task<Result<Guid>> Handle(UpdateUserCommand user,
        CancellationToken cancellationToken)
    {
        var result = await _userServices.UpdateUserAsync(user.Id, user, cancellationToken);

        return Result<Guid>.Success(result);
    }
}