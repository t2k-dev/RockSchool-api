using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.TeacherService;
using RockSchool.BL.Services.UserService;
using RockSchool.WebApi.Models;
using TeacherDto = RockSchool.BL.Dtos.TeacherDto;

namespace RockSchool.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherController : Controller
{
    private readonly ITeacherService _teacherService;
    private readonly IUserService _userService;

    public TeacherController(ITeacherService teacherService, IUserService userService)
    {
        _userService = userService;
        _teacherService = teacherService;
    }

    [EnableCors("MyPolicy")]
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var teachers = await _teacherService.GetAllTeachersAsync();

        if (teachers.Length == 0) return NotFound();

        return Ok(teachers);
    }

    [EnableCors("MyPolicy")]
    [HttpPost("addTeacher")]
    public async Task<ActionResult> addTeacher([FromBody] RegisterTeacherRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            throw new Exception("Incorrect requestDto for registration.");

        //TODO: uncomment and fix this mess)

        //var addUserServiceDto = new AddUserServiceRequestDto
        //{
        //    Login = requestDto.Login,
        //    RoleId = (int)UserRole.Teacher
        //};

        //var newUserId = await _userService.AddUserAsync(addUserServiceDto);

        var newTeacher = new TeacherDto
        {
            FirstName = requestDto.Teacher.FirstName,
            LastName = requestDto.Teacher.LastName,
            MiddleName = requestDto.Teacher.MiddleName,
            BirthDate = Convert.ToDateTime(requestDto.Teacher.BirthDate).ToUniversalTime(),
            Phone = requestDto.Teacher.Phone,
            //UserId = newUserId,
            //Disciplines = requestDto.Teacher.Disciplines,
            // WorkingHoursEntity = requestDto.WorkingHoursEntity
        };

        await _teacherService.AddTeacher(newTeacher);

        return Ok();
    }

    [EnableCors("MyPolicy")]
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var teacher = await _teacherService.GetTeacherByIdAsync(id);
        
        var result = new GetTeacherResponseDto
        {
            Email = teacher.User.Login,
            FirstName = teacher.FirstName,
            LastName = teacher.LastName,
            MiddleName = teacher.MiddleName,
            BirthDate = teacher.BirthDate,
            Phone = teacher.Phone,
            Disciplines = teacher.Disciplines.Select(d => d.DisciplineId).ToArray()
        };

        return Ok(result);
    }

    // TODO: We already add teacherEntity in account controller
    // [EnableCors("MyPolicy")]
    // [HttpPost]
    // public ActionResult Post(AddTeacherDto model)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //
    //     var newTeacher = new AddTeacherServiceRequestDto()
    //     {
    //         FirstName = model.FirstName,
    //         LastName = model.LastName,
    //         MiddleName = model.MiddleName,
    //         BirthDate = model.BirthDate,
    //         UserId = model.UserId,
    //         Disciplines = new List<DisciplineEntity>()
    //     };
    //
    //     _teacherService.AddTeacher(newTeacher);
    //
    //     foreach (var disciplineId in model.Disciplines)
    //     {
    //         var disciplineEntity = _context.Disciplines.SingleOrDefault(d => d.AttendanceId == disciplineId);
    //         newTeacher.Disciplines.Add(disciplineEntity);
    //     }
    //
    //     _context.Teachers.Add(newTeacher);
    //     _context.SaveChanges();
    //
    //     return Ok();
    // }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] AddTeacherDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updateRequest = new TeacherDto()
        {
            TeacherId = id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            MiddleName = model.MiddleName,
            BirthDate = model.BirthDate,
            Disciplines = model.Disciplines
        };

        await _teacherService.UpdateTeacherAsync(updateRequest);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _teacherService.DeleteTeacherAsync(id);

        return Ok();
    }
}