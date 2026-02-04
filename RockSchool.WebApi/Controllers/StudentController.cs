using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.BandStudentService;
using RockSchool.BL.Services.StudentService;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.WebApi.Helpers;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Attendances;
using RockSchool.WebApi.Models.Students;

namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]")]
[ApiController]
public class StudentController(
    IStudentService studentService,
    IAttendanceService attendanceService,
    ISubscriptionService subscriptionService,
    IBandStudentService bandStudentService)
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
        var student = await studentService.GetByIdAsync(id);
        var attendances = await attendanceService.GetAttendancesByStudentId(id);

        var attendanceInfos = new List<AttendanceInfo>();
        foreach (var attendance in attendances)
        {
            var subscription = attendance.Attendees;

            attendanceInfos.Add(new AttendanceInfo
            {
                AttendanceId = attendance.AttendanceId,
                StartDate = attendance.StartDate,
                EndDate = attendance.EndDate,
                Status = (int)attendance.Status,
                Teacher = attendance.Teacher,
                IsCompleted = attendance.IsCompleted,
                RoomId = attendance.RoomId,
                DisciplineId = attendance.DisciplineId,
                AttendanceType = (int)attendance.AttendanceType,
                Comment = attendance.Comment,
                GroupId = attendance.GroupId,
                //Students = 
            });
        }

        var subscriptions = await subscriptionService.GetSubscriptionsByStudentId(id);
        var subscriptionsInfos = subscriptions.Select(subscription => subscription.ToInfo());

        var bandStudents = await bandStudentService.GetByStudentIdAsync(id);
        var bands = bandStudents.Select(bs => bs.Band).Where(b => b != null).ToArray();
        var bandsInfos = bands.ToInfos();

        var studentScreenDetailsDto = new StudentScreenDetailsInfo
        {
            Student = student,
            Subscriptions = subscriptionsInfos.ToArray(),
            Attendances = attendanceInfos.ToArray(),
            Bands = bandsInfos,
        };

        return Ok(studentScreenDetailsDto);
    }

    [HttpPost]
    public async Task<ActionResult> AddStudent([FromBody] RegisterStudentRequestDto requestDto)
    {
        throw new NotImplementedException();
        /*
        if (!ModelState.IsValid)
            throw new Exception("Incorrect requestDto for registration.");

        var newStudent = new Student()
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            BirthDate = requestDto.BirthDate.ToUniversalTime(),
            Sex = requestDto.Sex,
            Phone = requestDto.Phone ?? 0, //TODO: fix
            Level = requestDto.Level,
            BranchId = requestDto.BranchId
        };

        var id = await studentService.AddStudentAsync(newStudent);

        return Ok(id);*/
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, UpdateStudentRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        throw new AbandonedMutexException();
        /*
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

        return Ok(id);*/
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await studentService.DeleteStudentAsync(id);

        return Ok();
    }
}