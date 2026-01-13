using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RockSchool.BL.Services.AuthorizationService;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;
using RockSchool.WebApi.Models.Account;

namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RockSchoolContext _context;
    private readonly AuthService _authService;

    public AuthController(
        UserManager<UserEntity> userManager,
        RockSchoolContext context,
        AuthService authService)
    {
        _userManager = userManager;
        _context = context;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            return BadRequest(new { message = "User with this email already exists" });

        var user = new UserEntity
        {
            UserId = Guid.NewGuid(),
            UserName = request.Email,
            Email = request.Email,
            Login = request.Email,
            RoleId = request.RoleId,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == request.RoleId);
        if (role != null)
        {
            await _userManager.AddToRoleAsync(user, role.Name);
        }

        return Ok(new { message = "User registered successfully", userId = user.UserId });
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
