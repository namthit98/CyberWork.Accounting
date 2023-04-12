using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Roles.DTOs;
using MediatR;

namespace CyberWork.Accounting.Application.Roles.Queries.GetRole;

public class GetRoleQueryHandler
    : IRequestHandler<GetRoleQuery, Result<RoleDto>>
{
    private readonly IRoleServices _roleServices;

    public GetRoleQueryHandler(IRoleServices roleServices)
    {
        _roleServices = roleServices ?? throw new ArgumentNullException(nameof(roleServices));
    }

    public async Task<Result<RoleDto>> Handle(GetRoleQuery queries,
            CancellationToken cancellationToken)
    {
        var result = await _roleServices.GetRoleAsync(queries.Id);

        return Result<RoleDto>.Success(result);
    }
}