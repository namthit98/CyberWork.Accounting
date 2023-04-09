using AutoMapper;
using CyberWork.Accounting.Application.Common.Exceptions;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Application.Organizations.Commands.DeleteOrganization;

public class DeleteOrganizationCommandHandler
    : IRequestHandler<DeleteOrganizationCommand, Result<Guid>>
{

    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public DeleteOrganizationCommandHandler(
         IMapper mapper,
        IApplicationDbContext context
    )
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(_context));
    }
    public async Task<Result<Guid>> Handle(DeleteOrganizationCommand request,
        CancellationToken cancellationToken)
    {

        var entity = await _context.Organizations
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException($"Đơn vị không tồn tại");
        }

        if (entity.UnderOrganizationId.CompareTo(Guid.Empty) == 0)
        {
            throw new ConflictException($"Đơn vị không thể xoá");
        }

        var isUsed = await _context.Organizations
            .AnyAsync(x => x.UnderOrganizationId == request.Id);

        if (isUsed)
        {
            throw new ConflictException("Đơn vị đang được sử dụng");
        }

        _context.Organizations.Remove(entity);

        var result = await _context.SaveChangesAsync(cancellationToken) > 0;

        return Result<Guid>.Success(entity.Id);
    }
}