using Microsoft.AspNetCore.Identity;
using RockSchool.BL.Services.EmailService;
using RockSchool.Domain.Entities;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Account;
using RockSchool.WebApi.Security.Passwords;

namespace RockSchool.WebApi.Services.Auth;

public class AuthRegistrationService : IAuthRegistrationService
{
    private readonly IEmailService _emailService;
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IUserAccountService _userAccountService;
    private readonly ILookupNormalizer _lookupNormalizer;
    private readonly UserManager<User> _userManager;

    public AuthRegistrationService(
        UserManager<User> userManager,
        ILookupNormalizer lookupNormalizer,
        IUserAccountService userAccountService,
        IPasswordGenerator passwordGenerator,
        IEmailService emailService)
    {
        _userManager = userManager;
        _lookupNormalizer = lookupNormalizer;
        _userAccountService = userAccountService;
        _passwordGenerator = passwordGenerator;
        _emailService = emailService;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterUserRequestDto request, CancellationToken cancellationToken = default)
    {
        var login = AccountLoginNormalizer.NormalizeEmailLogin(request.Login);
        var role = await _userAccountService.GetActiveRoleAsync(request.RoleId, cancellationToken);

        if (await _userAccountService.ExistsByLoginAsync(login, cancellationToken))
            throw new InvalidOperationException("User with this login already exists.");

        var generatedPassword = _passwordGenerator.Generate();
        var user = User.Create(login, role.RoleId);

        user.Email = login;
        user.NormalizedEmail = _lookupNormalizer.NormalizeEmail(login);

        var createResult = await _userManager.CreateAsync(user, generatedPassword);
        if (!createResult.Succeeded)
            throw new InvalidOperationException(string.Join("; ", createResult.Errors.Select(error => error.Description)));

        try
        {
            await _emailService.SendPasswordEmailAsync(login, login, generatedPassword);
        }
        catch
        {
            await _userManager.DeleteAsync(user);
            throw;
        }

        return new RegisterResponse
        {
            Login = login,
            Message = "Account created. The generated password was sent to the specified email."
        };
    }
}
