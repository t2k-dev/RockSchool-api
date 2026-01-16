using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.AuthorizationService;
using RockSchool.BL.Services.UserService;
using RockSchool.WebApi.Models.Account;

namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(
        IAuthService authService,
        IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userService.RegisterUserAsync(
            email: request.Email,
            roleId: request.RoleId
        );

        if (!result.Success)
            return BadRequest(new { message = result.Message });

        return Ok(new { message = result.Message, userId = result.UserId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var loginResponse = await _authService.LoginAsync(request.Login, request.Password);

        if (!loginResponse.Success)
            return Unauthorized(new { message = loginResponse.Message });

        return Ok(new
        {
            token = loginResponse.Token,
            email = loginResponse.Email,
            roleId = loginResponse.RoleId,
            roleName = loginResponse.RoleName
        });
    }
}
