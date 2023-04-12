using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommandHandler
    : IRequestHandler<UpdateRoleCommand, Result<Guid>>
{
    private readonly IRoleServices _roleServices;

    public UpdateRoleCommandHandler(IRoleServices roleServices)
    {
        _roleServices = roleServices ?? throw new ArgumentNullException(nameof(roleServices));
    }

    public async Task<Result<Guid>> Handle(UpdateRoleCommand role,
      CancellationToken cancellationToken)
    {
        var result = await _roleServices
            .UpdateRoleAsync(role.Id, role.Name, role.Description);

        return Result<Guid>.Success(result);
    }
}