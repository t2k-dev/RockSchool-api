using Microsoft.AspNetCore.Identity;
using RockSchool.Domain.Entities;
using RockSchool.WebApi.Models.Account;
using RockSchool.WebApi.Security.Tokens;

namespace RockSchool.WebApi.Services.Auth;

public class AuthLoginService : IAuthLoginService
{
    private readonly IUserAccountService _userAccountService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly UserManager<User> _userManager;

    public AuthLoginService(
        UserManager<User> userManager,
        IUserAccountService userAccountService,
        IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _userAccountService = userAccountService;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var login = AccountLoginNormalizer.NormalizeEmailLogin(request.Login);
        var user = await _userAccountService.FindByLoginAsync(login, cancellationToken);

        if (user is null || !user.IsActive)
            throw new UnauthorizedAccessException("Invalid login or password.");

        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isValidPassword)
            throw new UnauthorizedAccessException("Invalid login or password.");

        return _jwtTokenService.CreateToken(user, user.Role.RoleName);
    }
}
