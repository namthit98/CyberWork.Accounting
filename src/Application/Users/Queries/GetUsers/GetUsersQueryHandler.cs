using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Users.DTOs;
using MediatR;

namespace CyberWork.Accounting.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler
    : IRequestHandler<GetUsersQuery, Result<PaginatedList<UserDto>>>
{
    private readonly IUserServices _userServices;

    public GetUsersQueryHandler(IUserServices userServices)
    {
        _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
    }

    public async Task<Result<PaginatedList<UserDto>>> Handle(GetUsersQuery queries,
            CancellationToken cancellationToken)
    {
        var result = await _userServices.GetUsersAsync(queries.SearchValue,
            queries.PageNumber, queries.PageSize, cancellationToken);

        return Result<PaginatedList<UserDto>>.Success(result);
    }
}