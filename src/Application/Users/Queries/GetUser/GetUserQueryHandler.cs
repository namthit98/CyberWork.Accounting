using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Users.DTOs;
using MediatR;

namespace CyberWork.Accounting.Application.Users.Queries.GetUser;

public class GetUserQueryHandler
    : IRequestHandler<GetUserQuery, Result<UserDto>>
{
    private readonly IUserServices _userServices;

    public GetUserQueryHandler(IUserServices userServices)
    {
        _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
    }

    public async Task<Result<UserDto>> Handle(GetUserQuery queries,
            CancellationToken cancellationToken)
    {
        var result = await _userServices.GetUserAsync(queries.Id);

        return Result<UserDto>.Success(result);
    }
}