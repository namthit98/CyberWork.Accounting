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
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateOrganizationCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateOrganizationCommand request,
       CancellationToken cancellationToken)
    {
        var result = await _context.Organizations
            .Where(x => x.Code == request.Code)
            .FirstOrDefaultAsync(cancellationToken);

        if (result != null)
        {
            throw new ConflictException(nameof(Organization),
                nameof(CreateOrganizationCommand.Code), request.Code!);
        }

        var entity = _mapper.Map<Organization>(request);

        _context.Organizations.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(entity.Id);
    }
}