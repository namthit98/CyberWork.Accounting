using AutoMapper;
using CyberWork.Accounting.Application.Common.Exceptions;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Application.Organizations.Commands.CreateOrganization;


public class CreateOrganizationCommandHandler
    : IRequestHandler<CreateOrganizationCommand, Result<Guid>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public CreateOrganizationCommandHandler(
        IMapper mapper,
        IApplicationDbContext context
    )
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(_context));
    }

    public async Task<Result<Guid>> Handle(CreateOrganizationCommand organization,
       CancellationToken cancellationToken)
    {
        var org = await _context.Organizations
            .FirstOrDefaultAsync(x => x.Code == organization.Code, cancellationToken);

        if (org != null)
        {
            throw new ConflictException($"Mã {organization.Code} đã tồn tại.");
        }

        var entity = _mapper.Map<Organization>(organization);

        _context.Organizations.Add(entity);

        var result = await _context.SaveChangesAsync(cancellationToken) > 0;

        return Result<Guid>.Success(entity.Id);
    }
}