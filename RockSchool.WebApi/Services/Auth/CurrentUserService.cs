using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RockSchool.Domain.Entities;
using RockSchool.WebApi.Models.Account;

namespace RockSchool.WebApi.Services.Auth;

public class CurrentUserService : ICurrentUserService
{
    private readonly IUserAccountService _userAccountService;
    private readonly UserManager<User> _userManager;

    public CurrentUserService(UserManager<User> userManager, IUserAccountService userAccountService)
    {
        _userManager = userManager;
        _userAccountService = userAccountService;
    }

    public async Task<CurrentUserResponse> GetCurrentUserAsync(
        ClaimsPrincipal principal,
        CancellationToken cancellationToken = default)
    {
        var userIdValue = _userManager.GetUserId(principal);
        if (!Guid.TryParse(userIdValue, out var userId))
            throw new UnauthorizedAccessException("Invalid access token.");

        var user = await _userAccountService.FindByIdAsync(userId, cancellationToken);
        if (user is null)
            throw new UnauthorizedAccessException("User was not found.");

        return new CurrentUserResponse
        {
            UserId = user.Id,
            Login = user.Login,
            RoleId = user.RoleId,
            Role = user.Role.RoleName,
            IsActive = user.IsActive
        };
    }
}
