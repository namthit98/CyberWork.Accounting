using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Users.Commands.CreateUser;
using CyberWork.Accounting.Application.Users.Commands.UpdateUser;
using CyberWork.Accounting.Application.Users.DTOs;

namespace CyberWork.Accounting.Application.Common.Interfaces;

public interface IUserServices
{
    Task<Guid> CreateUserAsync(CreateUserCommand createUserDto,
        CancellationToken cancellationToken);
    Task<Guid> UpdateUserAsync(Guid id, UpdateUserCommand updateUserDto,
        CancellationToken cancellationToken);
    Task<Guid> DeleteUserAsync(Guid id);
    Task<UserDto> GetUserAsync(Guid id);
    Task<PaginatedList<UserDto>> GetUsersAsync(string SearchValue, int PageNumber,
        int PageSize, CancellationToken cancellationToken);
}