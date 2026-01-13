using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RockSchool.BL.Models;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.BL.Services.AuthorizationService;

public class AuthService : IAuthService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RockSchoolContext _context;
    private readonly JwtTokenService _jwtTokenService;

    public AuthService(
        UserManager<UserEntity> userManager,
        RockSchoolContext context,
        JwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _context = context;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginDetails> LoginAsync(string login, string password)
    {
        var user = await _userManager.FindByNameAsync(login) ?? await _userManager.FindByEmailAsync(login);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            return new LoginDetails { Success = false, Message = "Invalid login or password" };

        if (!user.IsActive)
            return new LoginDetails { Success = false, Message = "User account is inactive" };

        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == user.RoleId);
        var token = _jwtTokenService.GenerateJwtToken(user, role?.RoleName);

        return new LoginDetails
        {
            Success = true,
            Token = token,
            Email = user.Email,
            RoleId = user.RoleId,
            RoleName = role?.RoleName
        };
    }
}