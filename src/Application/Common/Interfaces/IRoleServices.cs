using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Roles.DTOs;

namespace CyberWork.Accounting.Application.Common.Interfaces;

public interface IRoleServices
{
    Task<Guid> CreateRoleAsync(string name, string description);
    Task<Guid> DeleteRoleAsync(Guid id);
    Task<RoleDto> GetRoleAsync(Guid id);
    Task<Guid> UpdateRoleAsync(Guid id, string name, string description);
    Task<PaginatedList<RoleDto>> GetRolesAsync(string SearchValue, int PageNumber,
        int PageSize, CancellationToken cancellationToken);
}