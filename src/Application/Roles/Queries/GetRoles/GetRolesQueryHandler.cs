using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Roles.DTOs;
using MediatR;

namespace CyberWork.Accounting.Application.Roles.Queries.GetRoles;

public class GetRolesQueryHandler
    : IRequestHandler<GetRolesQuery, Result<PaginatedList<RoleDto>>>
{
    private readonly IRoleServices _roleServices;

    public GetRolesQueryHandler(IRoleServices roleServices)
    {
        _roleServices = roleServices ?? throw new ArgumentNullException(nameof(roleServices));
    }

    public async Task<Result<PaginatedList<RoleDto>>> Handle(GetRolesQuery queries,
            CancellationToken cancellationToken)
    {
        var result = await _roleServices.GetRolesAsync(queries.SearchValue, queries.PageNumber,
            queries.PageSize, cancellationToken);

        return Result<PaginatedList<RoleDto>>.Success(result);
    }
}