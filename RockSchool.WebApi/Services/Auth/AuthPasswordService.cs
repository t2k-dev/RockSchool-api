using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RockSchool.Domain.Entities;
using RockSchool.WebApi.Models.Account;

namespace RockSchool.WebApi.Services.Auth;

public class AuthPasswordService : IAuthPasswordService
{
    private readonly IUserAccountService _userAccountService;
    private readonly UserManager<User> _userManager;

    public AuthPasswordService(UserManager<User> userManager, IUserAccountService userAccountService)
    {
        _userManager = userManager;
        _userAccountService = userAccountService;
    }

    public async Task ChangePasswordAsync(
        ClaimsPrincipal principal,
        ChangePasswordRequest request,
        CancellationToken cancellationToken = default)
    {
        var userIdValue = _userManager.GetUserId(principal);
        if (!Guid.TryParse(userIdValue, out var userId))
            throw new UnauthorizedAccessException("Invalid access token.");

        var user = await _userAccountService.FindByIdAsync(userId, cancellationToken);
        if (user is null || !user.IsActive)
            throw new UnauthorizedAccessException("User was not found.");

        var changePasswordResult = await _userManager.ChangePasswordAsync(
            user,
            request.CurrentPassword,
            request.NewPassword);

        if (!changePasswordResult.Succeeded)
            throw new InvalidOperationException(string.Join("; ", changePasswordResult.Errors.Select(error => error.Description)));
    }
}
