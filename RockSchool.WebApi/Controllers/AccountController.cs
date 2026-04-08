using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Account;
using RockSchool.WebApi.Security;
using RockSchool.WebApi.Services.Auth;

namespace RockSchool.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IAuthLoginService _authLoginService;
    private readonly IAuthPasswordService _authPasswordService;
    private readonly IAuthRegistrationService _authRegistrationService;

    public AccountController(
        IAuthRegistrationService authRegistrationService,
        IAuthLoginService authLoginService,
        IAuthPasswordService authPasswordService,
        ICurrentUserService currentUserService)
    {
        _authRegistrationService = authRegistrationService;
        _authLoginService = authLoginService;
        _authPasswordService = authPasswordService;
        _currentUserService = currentUserService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register(
        [FromBody] RegisterUserRequestDto requestDto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            throw new ArgumentException("Incorrect model for registration.");

        var response = await _authRegistrationService.RegisterAsync(requestDto, cancellationToken);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(
        [FromBody] LoginRequest requestDto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            throw new ArgumentException("Incorrect model for login.");

        var response = await _authLoginService.LoginAsync(requestDto, cancellationToken);
        return Ok(response);
    }

    [Authorize(Policy = AuthorizationPolicyNames.AuthenticatedUser)]
    [HttpGet("me")]
    public async Task<ActionResult<CurrentUserResponse>> Me(CancellationToken cancellationToken)
    {
        var response = await _currentUserService.GetCurrentUserAsync(User, cancellationToken);
        return Ok(response);
    }

    [Authorize(Policy = AuthorizationPolicyNames.AuthenticatedUser)]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(
        [FromBody] ChangePasswordRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            throw new ArgumentException("Incorrect model for changing password.");

        await _authPasswordService.ChangePasswordAsync(User, request, cancellationToken);
        return NoContent();
    }
}
