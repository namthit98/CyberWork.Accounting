using System.Security.Claims;
using API.DTOs;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Domain.Entities;
using CyberWork.Accounting.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenServices _tokenServices;
    public AccountController(UserManager<AppUser> userManager, ITokenServices tokenServices)
    {
        _tokenServices = tokenServices;
        _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AccountDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.Users.Include(u => u.UserProfile)
            .FirstOrDefaultAsync(x => x.Email == loginDto.Email);

        if (user == null) return Unauthorized();

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (result)
        {
            return CreateAccountObject(user);
        }

        return Unauthorized();
    }

    [HttpGet]
    public async Task<ActionResult<CurrentAccountDto>> GetCurrentAccount()
    {
        var user = await _userManager.Users
            .Include(u => u.UserProfile)
            .FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));

        return new CurrentAccountDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.UserProfile.FirstName,
            LastName = user.UserProfile.LastName,
        };
    }

    private AccountDto CreateAccountObject(AppUser user)
    {
        return new AccountDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.UserProfile.FirstName,
            LastName = user.UserProfile.LastName,
            Token = _tokenServices.CreateToken(user),
        };
    }
}