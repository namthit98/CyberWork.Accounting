using AutoMapper;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Organizations.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Application.Organizations.Queries.GetOrganization;

public class GetOrganizationQueryHandler
    : IRequestHandler<GetOrganizationQuery, Result<OrganizationDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetOrganizationQueryHandler(
        IMapper mapper,
        IApplicationDbContext context
    )
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(_context));
    }

    public async Task<Result<OrganizationDto>> Handle(GetOrganizationQuery queries,
        CancellationToken cancellationToken)
    {
         var entity = await _context.Organizations
            .FirstOrDefaultAsync(x => x.Id == queries.id);

        var result = _mapper.Map<OrganizationDto>(entity);

        return Result<OrganizationDto>.Success(result);
    }
}