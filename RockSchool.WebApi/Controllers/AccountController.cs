using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos;
using RockSchool.BL.Services.StudentService;
using RockSchool.BL.Services.TeacherService;
using RockSchool.BL.Services.UserService;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers;

[Route("api/[controller]")]
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
}