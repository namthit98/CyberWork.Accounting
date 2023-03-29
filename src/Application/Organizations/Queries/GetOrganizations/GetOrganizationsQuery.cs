using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Organizations.DTOs;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Queries.GetOrganizations;

public record GetOrganizationsQuery
    : IRequest<Result<PaginatedList<OrganizationDto>>>
{
    public string SearchValue { get; init; } = String.Empty;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
