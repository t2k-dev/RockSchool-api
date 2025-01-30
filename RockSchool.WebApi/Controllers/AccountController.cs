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

namespace RockSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;

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
            if (!ModelState.IsValid)
            {
                throw new Exception("Incorrect model for registration.");
            }

            var serviceDto = new AddUserServiceRequestDto
            {
                Login = requestDto.Login,
                Password = requestDto.Password,
                RoleId = requestDto.RoleId
            };

            await _userService.AddUserAsync(serviceDto);

            return Ok();
        }

        [EnableCors("MyPolicy")]
        [HttpPost("registerStudent")]
        public async Task<ActionResult> RegisterStudent([FromBody] RegisterStudentRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Incorrect requestDto for registration.");
            }

            var addUserServiceDto = new AddUserServiceRequestDto
            {
                Login = requestDto.Login,
                RoleId = (int)UserRole.Student
            };

            var newUserId = await _userService.AddUserAsync(addUserServiceDto);

            var newStudent = new AddStudentServiceRequestDto()
            {
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                BirthDate = requestDto.BirthDate,
                Sex = requestDto.Sex,
                Phone = requestDto.Phone,
                UserId = newUserId
            };

            await _studentService.AddStudentAsync(newStudent);

            return Ok();
        }

        [EnableCors("MyPolicy")]
        [HttpPost("registerTeacher")]
        public async Task<ActionResult> RegisterTeacher([FromBody] RegisterTeacherRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Incorrect requestDto for registration.");

            var addUserServiceDto = new AddUserServiceRequestDto()
            {
                Login = requestDto.Login,
                RoleId = (int)UserRole.Teacher
            };

            var newUserId = await _userService.AddUserAsync(addUserServiceDto);

            var newTeacher = new AddTeacherServiceRequestDto()
            {
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                MiddleName = requestDto.MiddleName,
                BirthDate = requestDto.BirthDate,
                Phone = requestDto.Phone,
                UserId = newUserId,
                Disciplines = requestDto.Disciplines,
                WorkingHoursEntity = requestDto.WorkingHoursEntity
            };

            await _teacherService.AddTeacher(newTeacher);

            return Ok();
        }
    }
}