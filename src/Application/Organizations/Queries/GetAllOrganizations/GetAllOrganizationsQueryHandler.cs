using AutoMapper;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Organizations.DTOs;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Queries.GetAllOrganizations;


public class GetAllOrganizationsQueryHandler
    : IRequestHandler<GetAllOrganizationsQuery, Result<List<OrganizationDto>>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public GetAllOrganizationsQueryHandler(IOrganizationRepository organizationRepository, IMapper mapper)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<Result<List<OrganizationDto>>> Handle(GetAllOrganizationsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _organizationRepository.GetAllOrganizationAsync(cancellationToken);

        return Result<List<OrganizationDto>>.Success(result);
    }
}