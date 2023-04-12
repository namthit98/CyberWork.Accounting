using AutoMapper;
using AutoMapper.QueryableExtensions;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Mappings;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Roles.DTOs;
using CyberWork.Accounting.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CyberWork.Accounting.Infrastructure.Identity;

public class RoleServices : IRoleServices
{
    private readonly IMapper _mapper;

    private readonly RoleManager<AppRole> _roleManager;

    public RoleServices(IMapper mapper, RoleManager<AppRole> roleManager)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    public async Task<Guid> CreateRoleAsync(string name, string description)
    {
        var role = new AppRole
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description
        };

        var result = await _roleManager.CreateAsync(role);

        if (result.Succeeded)
        {
            return role.Id;
        }

        return Guid.Empty;
    }

    public async Task<Guid> DeleteRoleAsync(Guid id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());

        if (role == null)
        {
            throw new Exception("Vai trò không tồn tại");
        }

        var result = await _roleManager.DeleteAsync(role);
        if (result.Succeeded)
        {
            return role.Id;
        }
        else
        {
            throw new Exception("Xoá vai trò thất bại");
        }
    }

    public async Task<RoleDto> GetRoleAsync(Guid id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());

        if (role == null)
        {
            throw new Exception("Vai trò không tồn tại");
        }

        var result = _mapper.Map<RoleDto>(role);

        return result;
    }

    public async Task<PaginatedList<RoleDto>> GetRolesAsync(
        string SearchValue, int PageNumber, int PageSize,
            CancellationToken cancellationToken)
    {
        var query = _roleManager.Roles
            .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
            .AsQueryable();

        if (!string.IsNullOrEmpty(SearchValue))
        {
            query = query.Where(x => x.Name.Contains(SearchValue));
        }

        var result = await query
            .PaginatedListAsync(PageNumber, PageSize, cancellationToken);

        return result;
    }

    public async Task<Guid> UpdateRoleAsync(Guid id, string name, string description)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());

        if (role == null)
        {
            throw new Exception("Vai trò không tồn tại");
        }

        role.Name = name ?? role.Name;
        role.Description = description ?? role.Description;

        var result = await _roleManager.UpdateAsync(role);

        if (result.Succeeded)
        {
            return role.Id;
        }
        else
        {
            throw new Exception("Cập nhật vai trò thất bại.");
        }
    }
}