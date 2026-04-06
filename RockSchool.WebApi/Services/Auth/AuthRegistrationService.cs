using Microsoft.AspNetCore.Identity;
using RockSchool.Domain.Entities;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Account;
using RockSchool.WebApi.Security.Passwords;

namespace RockSchool.WebApi.Services.Auth;

public class AuthRegistrationService : IAuthRegistrationService
{
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IUserAccountService _userAccountService;
    private readonly ILookupNormalizer _lookupNormalizer;
    private readonly UserManager<User> _userManager;

    public AuthRegistrationService(
        UserManager<User> userManager,
        ILookupNormalizer lookupNormalizer,
        IUserAccountService userAccountService,
        IPasswordGenerator passwordGenerator)
    {
        _userManager = userManager;
        _lookupNormalizer = lookupNormalizer;
        _userAccountService = userAccountService;
        _passwordGenerator = passwordGenerator;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterUserRequestDto request, CancellationToken cancellationToken = default)
    {
        var login = AccountLoginNormalizer.NormalizeEmailLogin(request.Login);
        var role = await _userAccountService.GetActiveRoleAsync(request.RoleId, cancellationToken);

        if (await _userAccountService.ExistsByLoginAsync(login, cancellationToken))
            throw new InvalidOperationException("User with this login already exists.");

        var generatedPassword = _passwordGenerator.Generate();
        var user = User.Create(login, request.FirstName, request.LastName, role.RoleId);

        user.Email = login;
        user.NormalizedEmail = _lookupNormalizer.NormalizeEmail(login);

        var createResult = await _userManager.CreateAsync(user, generatedPassword);
        if (!createResult.Succeeded)
            throw new InvalidOperationException(string.Join("; ", createResult.Errors.Select(error => error.Description)));

        return new RegisterResponse
        {
            Login = login,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = generatedPassword,
            Message = "Account created. Use the generated password to sign in."
        };
    }
}
