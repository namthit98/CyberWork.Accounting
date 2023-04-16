using AutoMapper;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Domain.Entities;
using MediatR;

namespace CyberWork.Accounting.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IMapper _mapper;
    private readonly IUserServices _userServices;
    private readonly IApplicationDbContext _context;


    public CreateUserCommandHandler(
        IMapper mapper,
        IUserServices userServices,
        IApplicationDbContext context
    )
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userServices = userServices
            ?? throw new ArgumentNullException(nameof(userServices));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand user,
        CancellationToken cancellationToken)
    {
        var result = await _userServices
            .CreateUserAsync(user, cancellationToken);

        return Result<Guid>.Success(result);
    }
}