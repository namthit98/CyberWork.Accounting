using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Roles.Commands.DeleteRole;

public class DeleteRoleCommandHandler
    : IRequestHandler<DeleteRoleCommand, Result<Guid>>
{
    private readonly IRoleServices _roleServices;

    public DeleteRoleCommandHandler(IRoleServices roleServices)
    {
        _roleServices = roleServices ?? throw new ArgumentNullException(nameof(roleServices));
    }

    public async Task<Result<Guid>> Handle(DeleteRoleCommand role,
        CancellationToken cancellationToken)
    {
        var result = await _roleServices.DeleteRoleAsync(role.Id);

        return Result<Guid>.Success(result);
    }
}
