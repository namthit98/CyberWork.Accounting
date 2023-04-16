using AutoMapper;
using AutoMapper.QueryableExtensions;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Mappings;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Organizations.DTOs;
using MediatR;

namespace CyberWork.Accounting.Application.Organizations.Queries.GetOrganizations;


public class GetOrganizationsQueryHandler
    : IRequestHandler<GetOrganizationsQuery, Result<PaginatedList<OrganizationDto>>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetOrganizationsQueryHandler(IMapper mapper,
        IApplicationDbContext context)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Result<PaginatedList<OrganizationDto>>> Handle(GetOrganizationsQuery queries,
        CancellationToken cancellationToken)
    {
        var query = _context.Organizations
           .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
           .AsQueryable();

        if (!String.IsNullOrEmpty(queries.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(queries.SearchValue));
        }

        var result = await query.PaginatedListAsync(
            queries.PageNumber, queries.PageSize, cancellationToken);

        return Result<PaginatedList<OrganizationDto>>.Success(result);
    }
}