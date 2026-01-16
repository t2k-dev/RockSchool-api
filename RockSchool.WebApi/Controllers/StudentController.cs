using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Models;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.BranchService;
using RockSchool.BL.Services.StudentService;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.WebApi.Helpers;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Attendances;
using RockSchool.WebApi.Models.Students;
using RockSchool.WebApi.Models.Subscriptions;

namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]")]
[ApiController]
public class StudentController(
    IStudentService studentService,
    IAttendanceService attendanceService,
    ISubscriptionService subscriptionService)
    : Controller
{

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var studentsDto = await studentService.GetAllStudentsAsync();

        if (studentsDto?.Length == 0)
            return NotFound();

        return Ok(studentsDto);
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
        var studentDto = await studentService.GetByIdAsync(id);
        var attendances = await attendanceService.GetAttendancesByStudentId(id);

        var attendanceInfos = new List<AttendanceInfo>();
        foreach (var attendance in attendances)
        {
            attendanceInfos.Add(new AttendanceInfo
            {
                AttendanceId = attendance.AttendanceId,
                StartDate = attendance.StartDate,
                EndDate = attendance.EndDate,
                Status = (int)attendance.Status,
                Teacher = attendance.Teacher,
                StudentId = attendance.StudentId,
                Student = attendance.Student,
                IsCompleted = attendance.IsCompleted,
                IsTrial = attendance.IsTrial,
                RoomId = attendance.RoomId,
                DisciplineId = attendance.DisciplineId,
                SubscriptionId = attendance.SubscriptionId,
            });
        }

        var subscriptions = await subscriptionService.GetSubscriptionsByStudentId(id);
        var subscriptionsInfos = subscriptions.Select(subscription => subscription.ToInfo());

        var studentScreenDetailsDto = new StudentScreenDetailsInfo
        {
            Student = studentDto,
            Subscriptions = subscriptionsInfos.ToArray(),
            Attendances = attendanceInfos.ToArray(),
        };

        return Ok(studentScreenDetailsDto);
    }

    [HttpPost]
    public async Task<ActionResult> AddStudent([FromBody] RegisterStudentRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newStudent = new Student()
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            BirthDate = requestDto.BirthDate.ToUniversalTime(),
            Sex = requestDto.Sex,
            Phone = requestDto.Phone ?? 0,
            Level = requestDto.Level,
            BranchId = requestDto.BranchId
        };

        var result = await studentService.AddStudentAsync(newStudent, requestDto.Email);

        if (!result.Success)
            return BadRequest(new { message = result.Message });

        return Ok(new
        {
            message = result.Message,
            studentId = result.StudentId,
            userId = result.UserId
        });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, UpdateStudentRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updateStudentDto = new Student()
        {
            StudentId = id,
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            BirthDate = requestDto.BirthDate,
            Sex = requestDto.Sex,
            Phone = requestDto.Phone,
            Level = requestDto.Level
        };
        await studentService.UpdateStudentAsync(updateStudentDto);

        return Ok(id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await studentService.DeleteStudentAsync(id);

        return Ok();
    }
}