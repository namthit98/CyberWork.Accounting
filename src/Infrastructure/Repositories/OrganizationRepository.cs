using AutoMapper;
using AutoMapper.QueryableExtensions;
using CyberWork.Accounting.Application.Common.Exceptions;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Mappings;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Organizations.Commands.CreateOrganization;
using CyberWork.Accounting.Application.Organizations.Commands.UpdateOrganization;
using CyberWork.Accounting.Application.Organizations.DTOs;
using CyberWork.Accounting.Application.Organizations.Queries.GetOrganization;
using CyberWork.Accounting.Application.Organizations.Queries.GetOrganizations;
using CyberWork.Accounting.Domain.Entities;
using CyberWork.Accounting.Infrastructure.Common;
using CyberWork.Accounting.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Infrastructure.Repositories;

public class OrganizationRepository : RepositoryBase<Organization, Guid, ApplicationDbContext>,
    IOrganizationRepository
{
    private readonly IMapper _mapper;
    public OrganizationRepository(
        IMapper mapper,
        ApplicationDbContext dbContext,
        IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Guid> CreateOrganizationAsync(CreateOrganizationCommand organization,
        CancellationToken cancellationToken)
    {
        var result = await FindByCondition(x => x.Code == organization.Code)
           .FirstOrDefaultAsync(cancellationToken);

        if (result != null)
        {
            throw new ConflictException(nameof(Organization),
                nameof(CreateOrganizationCommand.Code), organization.Code);
        }

        var entity = _mapper.Map<Organization>(organization);

        return await CreateAsync(entity, cancellationToken);
    }

    public async Task<Boolean> DeleteOrganizationAsync(Guid organizationId,
        CancellationToken cancellationToken)
    {
        var entity = await FindByCondition(x => x.Id == organizationId)
           .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Organization), organizationId);
        }

        await DeleteAsync(entity, cancellationToken);

        return true;
    }

    public async Task<List<OrganizationDto>>
        GetAllOrganizationAsync(CancellationToken cancellationToken)
    {
        var result = await FindAll()
            .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<OrganizationDto> GetOrganizationAsync(GetOrganizationQuery queries,
            CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(queries.id);

        var result = _mapper.Map<OrganizationDto>(entity);

        return result;
    }

    public async Task<PaginatedList<OrganizationDto>>
        GetOrganizationsAsync(GetOrganizationsQuery queries, CancellationToken cancellationToken)
    {
        var result = await FindAll()
            .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(queries.PageNumber, queries.PageSize, cancellationToken);

        return result;
    }

    public async Task<Guid>
        UpdateOrganizationAsync(UpdateOrganizationCommand organization,
            CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(organization.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Organization), organization.Id);
        }

        entity.Name = organization.Name ?? entity.Name;
        entity.ShortName = organization.ShortName ?? entity.ShortName;
        entity.Address = organization.Address ?? entity.Address;

        await UpdateAsync(entity, cancellationToken);
        return entity.Id;
    }
}