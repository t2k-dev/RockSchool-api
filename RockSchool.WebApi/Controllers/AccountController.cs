using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.StudentService;
using RockSchool.BL.Services.TeacherService;
using RockSchool.BL.Services.UserService;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Account;
using System;
using System.Threading.Tasks;
using RockSchool.BL.Models;

namespace RockSchool.WebApi.Controllers;

[Route("api/[controller]")]
[EnableCors("MyPolicy")]
[ApiController]
public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    // Not in use
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserRequestDto requestDto)
    {
        if (!ModelState.IsValid) throw new Exception("Incorrect model for registration.");

        var serviceDto = new UserDto()
        {
            Login = requestDto.Login,
            RoleId = requestDto.RoleId
        };

        await _userService.AddUserAsync(serviceDto);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest requestDto)
    {
        if (!ModelState.IsValid) throw new Exception("Incorrect model for registration.");


        return Ok("tutBudetToken");
    }
}