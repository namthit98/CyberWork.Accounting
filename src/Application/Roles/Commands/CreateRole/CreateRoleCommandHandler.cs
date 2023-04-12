using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using MediatR;

namespace CyberWork.Accounting.Application.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler
    : IRequestHandler<CreateRoleCommand, Result<Guid>>
{
    private readonly IRoleServices _roleServices;

    public CreateRoleCommandHandler(IRoleServices roleServices)
    {
        _roleServices = roleServices ?? throw new ArgumentNullException(nameof(roleServices));
    }

    public async Task<Result<Guid>> Handle(CreateRoleCommand role,
      CancellationToken cancellationToken)
    {
        var result = await _roleServices
            .CreateRoleAsync(role.Name, role.Description);

        return Result<Guid>.Success(result);
    }
}