using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Organizations.DTOs;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Queries.GetAllOrganizations;

public record GetAllOrganizationsQuery : IRequest<Result<List<OrganizationDto>>>
{
    public string SearchValue { get; init; }
}