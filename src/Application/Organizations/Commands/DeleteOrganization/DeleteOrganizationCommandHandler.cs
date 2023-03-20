using CyberWork.Accounting.Application.Common.Exceptions;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Application.Organizations.Commands.DeleteOrganization;

public class DeleteOrganizationCommandHandler
    : IRequestHandler<DeleteOrganizationCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;

    public DeleteOrganizationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<Unit>> Handle(DeleteOrganizationCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.Organizations
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Organization), request.Id);
        }

        _context.Organizations.Remove(entity);

        var result = await _context.SaveChangesAsync(cancellationToken) > 0;

        if (!result) return Result<Unit>.Failure("Failed to delete the Organization");

        return Result<Unit>.Success(Unit.Value);
    }
}