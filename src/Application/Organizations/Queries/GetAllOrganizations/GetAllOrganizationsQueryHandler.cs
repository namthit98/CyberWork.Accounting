using AutoMapper;
using AutoMapper.QueryableExtensions;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Organizations.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Application.Organizations.Queries.GetAllOrganizations;


public class GetAllOrganizationsQueryHandler
    : IRequestHandler<GetAllOrganizationsQuery, Result<List<OrganizationDto>>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetAllOrganizationsQueryHandler(
        IMapper mapper,
        IApplicationDbContext context)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(_context));
    }

    public async Task<Result<List<OrganizationDto>>> Handle(GetAllOrganizationsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Organizations
            .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
            .AsQueryable();

        if (!String.IsNullOrEmpty(request.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(request.SearchValue));
        }

        var result = await query.ToListAsync(cancellationToken);

        return Result<List<OrganizationDto>>.Success(result);
    }
}