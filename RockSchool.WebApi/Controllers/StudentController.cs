using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Bands;
using RockSchool.BL.Common.Models;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.BL.Students;
using RockSchool.BL.Students.AddStudent;
using RockSchool.WebApi.Helpers;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Attendances;
using RockSchool.WebApi.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]s")]
[ApiController]
public class StudentController(
    IStudentService studentService,
    IAttendanceService attendanceService,
    ISubscriptionService subscriptionService,
    IBandMemberService bandMemberService,
    IStudentScreenDetailsService studentScreenDetailsService,
    IAddStudentService addStudentService)
    : Controller
{

    [HttpGet]
    public async Task<ActionResult> GetAllStudents()
    {
        var students = await studentService.GetAllStudentsAsync();

        if (students?.Length == 0)
            return NotFound();

        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var student = await studentService.GetByIdAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }


    [HttpGet("{id}/screen-details")]
    public async Task<ActionResult> GetStudentScreenDetails(Guid id)
    {
        var details = await studentScreenDetailsService.Query(id);

        if (details.Student == null)
            return NotFound();

        var result = new StudentScreenDetailsResponse
        {
            Student = new StudentInfo
            {
                FirstName = details.Student.FirstName,
                LastName = details.Student.LastName,
                StudentId = details.Student.StudentId,
            },
            Attendances = details.Attendances,
            Subscriptions = details.Subscriptions.ToInfos().ToArray(),
            Bands = details.Bands.ToInfos()
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> AddStudent([FromBody] RegisterStudentRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var addStudentDto = new AddStudentDto
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            BirthDate = requestDto.BirthDate.ToUniversalTime(),
            Sex = requestDto.Sex,
            Phone = requestDto.Phone ?? 0,
            Level = requestDto.Level,
            BranchId = requestDto.BranchId
        };

        var id = await addStudentService.Handle(addStudentDto);

        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, UpdateStudentRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var studentPersonalDataDto = new PersonalDataDto
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            BirthDate = requestDto.BirthDate,
            Sex = requestDto.Sex,
            Phone = requestDto.Phone,
            Level = requestDto.Level
        };
        await studentService.UpdatePersonalDataAsync(id, studentPersonalDataDto);

        return Ok(id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await studentService.DeleteStudentAsync(id);

        return Ok();
    }
}