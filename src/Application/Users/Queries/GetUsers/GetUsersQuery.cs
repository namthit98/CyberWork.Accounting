using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Users.DTOs;
using MediatR;

namespace CyberWork.Accounting.Application.Users.Queries.GetUsers;

public record GetUsersQuery
    : IRequest<Result<PaginatedList<UserDto>>>
{
    public string SearchValue { get; init; } = string.Empty;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}