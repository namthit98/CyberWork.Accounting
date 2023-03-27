using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Organizations.Commands.CreateOrganization;
using CyberWork.Accounting.Application.Organizations.Commands.UpdateOrganization;
using CyberWork.Accounting.Application.Organizations.Commands.UpdateStatusOrganization;
using CyberWork.Accounting.Application.Organizations.DTOs;
using CyberWork.Accounting.Application.Organizations.Queries.GetOrganization;
using CyberWork.Accounting.Application.Organizations.Queries.GetOrganizations;

namespace CyberWork.Accounting.Application.Common.Interfaces;

public interface IOrganizationRepository
{
    Task<List<OrganizationDto>> GetAllOrganizationAsync(string searchValue, CancellationToken cancellationToken);
    Task<PaginatedList<OrganizationDto>> GetOrganizationsAsync(GetOrganizationsQuery queries,
        CancellationToken cancellationToken);
    Task<OrganizationDto> GetOrganizationAsync(GetOrganizationQuery queries,
        CancellationToken cancellationToken);
    Task<Guid> CreateOrganizationAsync(CreateOrganizationCommand organization,
        CancellationToken cancellationToken);
    Task<Guid> UpdateOrganizationAsync(UpdateOrganizationCommand organization,
        CancellationToken cancellationToken);
    Task<Guid> UpdateStatusOrganizationAsync(UpdateStatusOrganizationCommand organization,
        CancellationToken cancellationToken);
    Task<Guid> DeleteOrganizationAsync(Guid organizationId,
        CancellationToken cancellationToken);
}