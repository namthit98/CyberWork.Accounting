using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Organizations.DTOs;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Queries.GetOrganizations;


public class GetOrganizationsQueryHandler
    : IRequestHandler<GetOrganizationsQuery, Result<PaginatedList<OrganizationDto>>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public GetOrganizationsQueryHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<Result<PaginatedList<OrganizationDto>>> Handle(GetOrganizationsQuery queries,
        CancellationToken cancellationToken)
    {
        var result = await _organizationRepository.GetOrganizationsAsync(queries, cancellationToken);

        return Result<PaginatedList<OrganizationDto>>.Success(result);
    }
}