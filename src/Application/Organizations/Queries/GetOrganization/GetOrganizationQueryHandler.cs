using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Organizations.DTOs;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Queries.GetOrganization;

public class GetOrganizationQueryHandler
    : IRequestHandler<GetOrganizationQuery, Result<OrganizationDto>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public GetOrganizationQueryHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<Result<OrganizationDto>> Handle(GetOrganizationQuery queries,
        CancellationToken cancellationToken)
    {
        var result = await _organizationRepository.GetOrganizationAsync(queries,
            cancellationToken);

        return Result<OrganizationDto>.Success(result);
    }
}