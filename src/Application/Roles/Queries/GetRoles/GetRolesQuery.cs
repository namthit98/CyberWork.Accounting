using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Roles.DTOs;
using MediatR;

namespace CyberWork.Accounting.Application.Roles.Queries.GetRoles;

public record GetRolesQuery
    : IRequest<Result<PaginatedList<RoleDto>>>
{
    public string SearchValue { get; init; } = string.Empty;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
