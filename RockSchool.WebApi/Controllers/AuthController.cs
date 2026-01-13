using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly RockSchoolContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<UserEntity> userManager,
        RoleManager<RoleEntity> roleManager,
        RockSchoolContext context,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
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

        var user = await _userManager.FindByNameAsync(request.Login) ?? await _userManager.FindByEmailAsync(request.Login);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            return Unauthorized(new { message = "Invalid login or password" });

        if (!user.IsActive)
            return Unauthorized(new { message = "User account is inactive" });

        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == user.RoleId);
        var token = GenerateJwtToken(user, role?.RoleName);

        return Ok(new
        {
            token = token,
            email = user.Email,
            roleId = user.RoleId,
            roleName = role?.RoleName
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
