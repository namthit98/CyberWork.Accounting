using AutoMapper;
using AutoMapper.QueryableExtensions;
using CyberWork.Accounting.Application.Common.Exceptions;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Mappings;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Organizations.Commands.CreateOrganization;
using CyberWork.Accounting.Application.Organizations.Commands.UpdateOrganization;
using CyberWork.Accounting.Application.Organizations.Commands.UpdateStatusOrganization;
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
    private readonly ApplicationDbContext _dbContext;
    public OrganizationRepository(
        IMapper mapper,
        ApplicationDbContext dbContext,
        IUnitOfWork<ApplicationDbContext> unitOfWork) : base(dbContext, unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Guid> CreateOrganizationAsync(CreateOrganizationCommand organization,
        CancellationToken cancellationToken)
    {
        var result = await FindByCondition(x => x.Code == organization.Code)
           .FirstOrDefaultAsync(cancellationToken);

        if (result != null)
        {
            throw new ConflictException($"Mã {organization.Code} đã tồn tại.");
        }

        var entity = _mapper.Map<Organization>(organization);

        return await CreateAsync(entity, cancellationToken);
    }

    public async Task<Guid> DeleteOrganizationAsync(Guid organizationId,
        CancellationToken cancellationToken)
    {
        var entity = await FindByCondition(x => x.Id == organizationId)
           .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException($"Đơn vị không tồn tại");
        }

        if (entity.UnderOrganizationId.CompareTo(Guid.Empty) == 0)
        {
            throw new ConflictException($"Đơn vị không thể xoá");
        }

        var isUsed = await _dbContext.Organizations.AnyAsync(x => x.UnderOrganizationId == organizationId);

        if (isUsed)
        {
            throw new ConflictException("Đơn vị đang được sử dụng");
        }

        await DeleteAsync(entity, cancellationToken);

        return entity.Id;
    }

    public async Task<List<OrganizationDto>>
        GetAllOrganizationAsync(string searchValue, CancellationToken cancellationToken)
    {
        var query = FindAll()
            .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
            .AsQueryable();

        if (!String.IsNullOrEmpty(searchValue))
        {
            query = query.Where(x => x.Name.Contains(searchValue));
        }

        var result = await query.ToListAsync(cancellationToken);

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
        var query = FindAll()
            .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
            .AsQueryable();

        if (!String.IsNullOrEmpty(queries.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(queries.SearchValue));
        }

        var result = await query.PaginatedListAsync(
            queries.PageNumber, queries.PageSize, cancellationToken);

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

    public async Task<Guid>
        UpdateStatusOrganizationAsync(UpdateStatusOrganizationCommand organization,
        CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(organization.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Organization), organization.Id);
        }

        if (entity.UnderOrganizationId.CompareTo(Guid.Empty) == 0)
        {
            throw new ConflictException($"Đơn vị không thể huỷ kích hoạt");
        }

        entity.Status = organization.Status;

        await UpdateAsync(entity, cancellationToken);
        return entity.Id;
    }

}