using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos.Service.Requests.StudentService;
using RockSchool.BL.Dtos.Service.Requests.TeacherService;
using RockSchool.BL.Dtos.Service.Requests.UserService;
using RockSchool.BL.Services.StudentService;
using RockSchool.BL.Services.TeacherService;
using RockSchool.BL.Services.UserService;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Enums;

namespace RockSchool.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly IStudentService _studentService;
    private readonly ITeacherService _teacherService;
    private readonly IUserService _userService;

    public AccountController(IUserService userService, IStudentService studentService, ITeacherService teacherService)
    {
        _userService = userService;
        _studentService = studentService;
        _teacherService = teacherService;
    }

    // Not in use
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserRequestDto requestDto)
    {
        if (!ModelState.IsValid) throw new Exception("Incorrect model for registration.");

        var serviceDto = new AddUserServiceRequestDto
        {
            Login = requestDto.Login,
            Password = requestDto.Password,
            RoleId = requestDto.RoleId
        };

        await _userService.AddUserAsync(serviceDto);

        return Ok();
    }
}