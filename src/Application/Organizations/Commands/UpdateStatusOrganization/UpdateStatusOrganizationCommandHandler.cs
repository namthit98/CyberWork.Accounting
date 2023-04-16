using AutoMapper;
using CyberWork.Accounting.Application.Common.Exceptions;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Application.Organizations.Commands.UpdateStatusOrganization;

public class UpdateStatusOrganizationCommandHandler
    : IRequestHandler<UpdateStatusOrganizationCommand, Result<Guid>>
{

    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public UpdateStatusOrganizationCommandHandler(
        IMapper mapper,
        IApplicationDbContext context
    )
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<Result<Guid>> Handle(UpdateStatusOrganizationCommand organization,
      CancellationToken cancellationToken)
    {
        var entity = await _context.Organizations
          .FirstOrDefaultAsync(o => o.Id == organization.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Organization), organization.Id);
        }

        if (entity.UnderOrganizationId.CompareTo(Guid.Empty) == 0)
        {
            throw new ConflictException($"Đơn vị không thể huỷ kích hoạt");
        }

        entity.Status = organization.Status;

        _context.Organizations.Update(entity);

        var result = await _context.SaveChangesAsync(cancellationToken) > 0;

        return Result<Guid>.Success(entity.Id);
    }
}