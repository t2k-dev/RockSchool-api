using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.DisciplineService;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.BL.Services.TeacherService;
using RockSchool.BL.Services.UserService;
using RockSchool.WebApi.Factories;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Teachers;

namespace RockSchool.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherController : Controller
{
    private readonly ITeacherService _teacherService;
    private readonly IUserService _userService;
    private readonly IDisciplineService _disciplineService;
    private readonly IAttendanceService _attendanceService;
    private readonly ISubscriptionService _subscriptionService;

    public TeacherController(ITeacherService teacherService, IUserService userService,
        IDisciplineService disciplineService, IAttendanceService attendanceService, ISubscriptionService subscriptionService)
    {
        _userService = userService;
        _disciplineService = disciplineService;
        _attendanceService = attendanceService;
        _subscriptionService = subscriptionService;
        _teacherService = teacherService;
    }

    [EnableCors("MyPolicy")]
    [HttpPost]
    public async Task<ActionResult> AddTeacher([FromBody] RegisterTeacherRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            throw new Exception("Incorrect requestDto for registration.");

        var newTeacher = new TeacherDto
        {
            FirstName = requestDto.Teacher.FirstName,
            LastName = requestDto.Teacher.LastName,
            BirthDate = requestDto.Teacher.BirthDate.ToUniversalTime(),
            Phone = requestDto.Teacher.Phone,
            BranchId = requestDto.Teacher.BranchId,
            Sex = requestDto.Teacher.Sex,
            DisciplineIds = requestDto.Teacher.Disciplines,
            WorkingPeriods = requestDto.WorkingPeriods,
            AllowGroupLessons = requestDto.Teacher.AllowGroupLessons,
            AgeLimit = requestDto.Teacher.AgeLimit
        };

        await _teacherService.AddTeacher(newTeacher);

        return Ok();
    }

    [EnableCors("MyPolicy")]
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] TeacherFormDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updateRequest = new TeacherDto()
        {
            TeacherId = id,
            FirstName = model.Teacher.FirstName,
            LastName = model.Teacher.LastName,
            BirthDate = model.Teacher.BirthDate,
            Sex = model.Teacher.Sex,
            Phone = model.Teacher.Phone,
            AgeLimit = model.Teacher.AgeLimit,
            AllowGroupLessons = model.Teacher.AllowGroupLessons,
            DisciplineIds = model.Teacher.Disciplines
        };

        await _teacherService.UpdateTeacherAsync(updateRequest);

        return Ok();
    }

    [EnableCors("MyPolicy")]
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var teacher = await _teacherService.GetTeacherByIdAsync(id);

        var result = new TeacherInfo
        {
            Email = teacher.User?.Login,
            FirstName = teacher.FirstName,
            LastName = teacher.LastName,
            BirthDate = teacher.BirthDate,
            Sex = teacher.Sex,
            Phone = teacher.Phone,
            Disciplines = teacher.Disciplines?.Select(d => d.DisciplineId).ToArray(),
            AllowGroupLessons = teacher.AllowGroupLessons,
            AgeLimit = teacher.AgeLimit,
            BranchId = teacher.BranchId,
            WorkingPeriods = teacher.WorkingPeriods?.OrderBy(p => p.WeekDay).ToArray()
        };

        return Ok(result);
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
    [HttpGet("getTeacherScreenDetails/{id}")]
    public async Task<ActionResult> GetTeacherScreenDetails(Guid id)
    {
        var teacherDto = await _teacherService.GetTeacherByIdAsync(id);

        var attendances = await _attendanceService.GetAttendancesByTeacherIdForPeriodOfTime(
            id,
            DateTime.MinValue,
            DateTime.MaxValue);
        var attendanceInfos = new List<AttendanceInfo>();
        foreach (var attendance in attendances)
        {
            attendanceInfos.Add(new AttendanceInfo
            {
                StartDate = attendance.StartDate,
                EndDate = attendance.EndDate,
                Status = (int)attendance.Status,
                Student = attendance.Student,
                IsTrial = true,
                RoomId = attendance.RoomId,
                DisciplineId = attendance.DisciplineId,
            });
        }

        var subscriptions = await _subscriptionService.GetSubscriptionsByTeacherId(id);
        var subscriptionInfos = new List<SubscriptionInfo>();
        foreach (var subscription in subscriptions)
        {
            subscriptionInfos.Add(new SubscriptionInfo
            {
                StartDate = subscription.StartDate,
                Student = subscription.Student,
                Status = subscription.Status,
                DisciplineId = subscription.DisciplineId,
                TrialStatus = subscription.TrialStatus,
                IsTrial = true,
            });
        }

        // TODO: remap from DTO to Info
        var teacherScreenDetails = new TeacherScreenDetailsInfo
        {
            Teacher = new TeacherInfo
            {
                TeacherId = teacherDto.TeacherId,
                FirstName = teacherDto.FirstName,
                LastName = teacherDto.LastName,
                WorkingPeriods = teacherDto.WorkingPeriods.ToArray(),
                ScheduledWorkingPeriods = teacherDto.ScheduledWorkingPeriods.ToArray(),
                Disciplines = teacherDto.DisciplineIds.ToArray(),
            },
            Attendances = attendanceInfos.ToArray(),
            Subscriptions = subscriptionInfos.ToArray(),
        };

        return Ok(teacherScreenDetails);
    }

    [EnableCors("MyPolicy")]
    [HttpGet("getAvailableTeachers")]
    public async Task<ActionResult> GetAvailableTeachers(int disciplineId, int studentAge, int branchId)
    {
        var teachers = await _teacherService.GetAvailableTeachersAsync(disciplineId, branchId, studentAge);
        var attendanceMap = new Dictionary<Guid, AttendanceDto[]>();

        foreach (var teacher in teachers)
        {
            var attendances = await _attendanceService.GetAttendancesByTeacherIdForPeriodOfTime(
                teacher.TeacherId,
                DateTime.MinValue,
                DateTime.MaxValue);

            attendanceMap[teacher.TeacherId] = attendances ?? Array.Empty<AttendanceDto>();
        }

        var response = AvailableTeacherFactory.CreateResponse(teachers, attendanceMap);
        return Ok(response);
    }

    [EnableCors("MyPolicy")]
    [HttpGet("{id}/workingPeriods")]
    public async Task<ActionResult> GetTeacherWorkingPeriods(Guid id)
    {
        var teacher = await _teacherService.GetTeacherByIdAsync(id);

        var attendanceMap = new Dictionary<Guid, AttendanceDto[]>();

        var attendances = await _attendanceService.GetAttendancesByTeacherIdForPeriodOfTime(
            teacher.TeacherId,
            DateTime.MinValue,
            DateTime.MaxValue);

        attendanceMap[teacher.TeacherId] = attendances ?? Array.Empty<AttendanceDto>();

        var response = AvailableTeacherFactory.CreateResponse(new[] { teacher }, attendanceMap);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _teacherService.DeleteTeacherAsync(id);

        return Ok();
    }
}