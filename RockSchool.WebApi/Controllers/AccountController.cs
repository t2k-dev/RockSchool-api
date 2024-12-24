using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RockSchool.WebApi.Entities;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Services;

namespace RockSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : MyBaseController
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(RockSchoolContext rockSchoolContext, IMapper mapper, IPasswordHasher<User> passwordHasher)
            : base (rockSchoolContext, mapper)
        {
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Incorrect model for registration.");
            }

            var newUser = new User()
            {
                Login = registerUserDto.Login,
                RoleId = registerUserDto.RoleId,  
            };

            var passwordHash = _passwordHasher.HashPassword(newUser, registerUserDto.Password);
            newUser.PasswordHash = passwordHash;

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok();
        }

        [EnableCors("MyPolicy")]
        [HttpPost("registerStudent")]
        public ActionResult RegisterStudent([FromBody] RegisterStudentDto model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Incorrect model for registration.");
            }

            // Add user
            var usersService = new UserService(_context, _passwordHasher);
            var userId = usersService.AddUser(model.Login, (int) UserRole.Student);

            var newStudent = new AddStudentDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                Sex = model.Sex,
                Phone = model.Phone,
                UserId = userId
            };

            // Add student
            var studentService = new StudentService(_context, _mapper);
            studentService.AddStudent(newStudent);

            return Ok();
        }

        [EnableCors("MyPolicy")]
        [HttpPost("registerTeacher")]
        public ActionResult RegisterTeacher([FromBody] RegisterTeacherDto model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Incorrect model for registration.");
            }

            // Add user
            var usersService = new UserService(_context, _passwordHasher);
            var userId = usersService.AddUser(model.Login, (int) UserRole.Teacher);

            var newTeacher = new AddTeacherDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                BirthDate = model.BirthDate,
                Phone = model.Phone,
                UserId = userId,
                Disciplines = model.Disciplines,
                WorkingHours = model.WorkingHours
            };

            // Add teacher
            var teacherService = new TeacherService(_context, _mapper);
            var teacherId = teacherService.AddTeacher(newTeacher);

            return Ok();
        }
    }
}
