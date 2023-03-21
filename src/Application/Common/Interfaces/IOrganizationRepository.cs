using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Organizations.Commands.CreateOrganization;
using CyberWork.Accounting.Application.Organizations.DTOs;
using CyberWork.Accounting.Application.Organizations.Queries.GetOrganizations;

namespace CyberWork.Accounting.Application.Common.Interfaces;

public interface IOrganizationRepository
{
    Task<List<OrganizationDto>> GetAllOrganizationAsync(CancellationToken cancellationToken);
    Task<PaginatedList<OrganizationDto>> GetOrganizationsAsync(GetOrganizationsQuery queries,
        CancellationToken cancellationToken);
    Task<Guid> CreateOrganization(CreateOrganizationCommand organization,
        CancellationToken cancellationToken);
}