using AutoMapper;
using AutoMapper.QueryableExtensions;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Organizations.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Application.Organizations.Queries.GetAllOrganizations;


public class GetAllOrganizationsQueryHandler
    : IRequestHandler<GetAllOrganizationsQuery, Result<IEnumerable<OrganizationDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllOrganizationsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<OrganizationDto>>> Handle(GetAllOrganizationsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _context.Organizations
            .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return Result<IEnumerable<OrganizationDto>>.Success(result);
    }
}