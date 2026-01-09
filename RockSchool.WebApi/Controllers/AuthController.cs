using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RockSchool.Data.Entities;
using RockSchool.WebApi.Models.Auth;

namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<UserEntity> userManager,
        RoleManager<RoleEntity> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
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

        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
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

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            return Unauthorized(new { message = "Invalid email or password" });

        if (!user.IsActive)
            return Unauthorized(new { message = "User account is inactive" });

        var role = await _roleManager.FindByIdAsync(user.RoleId.ToString());
        var token = GenerateJwtToken(user, role?.RoleName);

        return Ok(new LoginResponse
        {
            Token = token,
            Email = user.Email,
            RoleId = user.RoleId,
            RoleName = role?.RoleName
        });
    }

    private string GenerateJwtToken(UserEntity user, string roleName)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, roleName ?? "Student"),
            new Claim("RoleId", user.RoleId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
