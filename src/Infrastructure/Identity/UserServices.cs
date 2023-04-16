using AutoMapper;
using AutoMapper.QueryableExtensions;
using CyberWork.Accounting.Application.Common.Exceptions;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Application.Common.Mappings;
using CyberWork.Accounting.Application.Common.Models;
using CyberWork.Accounting.Application.Users.Commands.CreateUser;
using CyberWork.Accounting.Application.Users.Commands.UpdateUser;
using CyberWork.Accounting.Application.Users.DTOs;
using CyberWork.Accounting.Domain.Entities;
using CyberWork.Accounting.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CyberWork.Accounting.Infrastructure.Identity;

public class UserServices : IUserServices
{
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IApplicationDbContext _context;

    public UserServices(IMapper mapper, IApplicationDbContext context,
        UserManager<AppUser> userManager)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<Guid> CreateUserAsync(CreateUserCommand createUserDto,
        CancellationToken cancellationToken)
    {
        var profileEntity = _mapper.Map<UserProfile>(createUserDto);
        _context.UserProfiles.Add(profileEntity);
        await _context.SaveChangesAsync(cancellationToken);

        var user = new AppUser
        {
            UserProfileId = profileEntity.Id,
            UserName = createUserDto.Email,
            Email = createUserDto.Email,
            PhoneNumber = createUserDto.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, "Dev@2023");

        if (result.Succeeded)
        {
            return user.Id;
        }

        throw new BadException("Tạo người dùng thất bại");
    }

    public async Task<Guid> DeleteUserAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null)
        {
            throw new BadException("Người dùng không tồn tại");
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return user.Id;
        }

        throw new BadException("Xoá người dùng thất bại");
    }

    public async Task<UserDto> GetUserAsync(Guid id)
    {
        var result = await _userManager.Users
            .Where(u => u.Id.CompareTo(id) == 0)
            .Include(u => u.UserProfile)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (result == null)
        {
            throw new BadException("người dùng không tồn tại");
        }

        return result;
    }

    public async Task<PaginatedList<UserDto>> GetUsersAsync(string SearchValue,
        int PageNumber, int PageSize, CancellationToken cancellationToken)
    {
        var query = _userManager.Users
            .Include(u => u.UserProfile)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .AsQueryable();

        if (!string.IsNullOrEmpty(SearchValue))
        {
            query = query.Where(x => x.Email.Contains(SearchValue.Trim()));
        }

        var result = await query
            .PaginatedListAsync(PageNumber, PageSize, cancellationToken);

        return result;
    }

    public async Task<Guid> UpdateUserAsync(Guid id, UpdateUserCommand updateUserDto,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        var userProfile = await _context.UserProfiles
            .FirstOrDefaultAsync(u =>
                u.Id.CompareTo(user.UserProfileId) == 0, cancellationToken);

        userProfile.FirstName = updateUserDto.FirstName ?? userProfile.FirstName;
        userProfile.LastName = updateUserDto.LastName ?? userProfile.LastName;
        userProfile.PersonalEmail = updateUserDto.PersonalEmail ?? userProfile.PersonalEmail;
        userProfile.Address = updateUserDto.Address ?? userProfile.Address;

        if(updateUserDto.Birthday != DateTime.MinValue)
        {
            userProfile.Birthday = updateUserDto.Birthday;
        }

        if( updateUserDto.Gender != Gender.Male &&
            updateUserDto.Gender != Gender.Female &&
            updateUserDto.Gender != Gender.Other
        )
        {
            userProfile.Gender = Gender.None;
        }
        else
        {
            userProfile.Gender = updateUserDto.Gender;
        }

        _context.UserProfiles.Update(userProfile);

        if (user == null)
        {
            throw new BadException("Vai trò không tồn tại");
        }

        user.PhoneNumber = updateUserDto.PhoneNumber ?? user.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }

        throw new BadException("Cập nhật người dùng thất bại.");
    }
}