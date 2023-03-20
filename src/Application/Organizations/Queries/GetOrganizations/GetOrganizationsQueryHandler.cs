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
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOrganizationsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<OrganizationDto>>> Handle(GetOrganizationsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _context.Organizations
            .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return Result<PaginatedList<OrganizationDto>>.Success(result);
    }
}