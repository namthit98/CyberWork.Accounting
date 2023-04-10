using AutoMapper;
using CyberWork.Accounting.Application.Common.Exceptions;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Application.Organizations.Commands.UpdateOrganization;

public class UpdateOrganizationCommandHandler
    : IRequestHandler<UpdateOrganizationCommand, Result<Guid>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public UpdateOrganizationCommandHandler(
        IMapper mapper,
        IApplicationDbContext context
    )
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(_context));
    }

    public async Task<Result<Guid>> Handle(UpdateOrganizationCommand organization,
       CancellationToken cancellationToken)
    {
        var entity = await _context.Organizations
            .FirstOrDefaultAsync(x => x.Id == organization.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Organization), organization.Id);
        }

        var updatedEntity = _mapper.Map(organization, entity);

        _context.Organizations.Update(updatedEntity);

        var result = await _context.SaveChangesAsync(cancellationToken) > 0;

        return Result<Guid>.Success(entity.Id);
    }
}