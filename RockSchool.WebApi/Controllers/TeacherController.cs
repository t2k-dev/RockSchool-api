using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.DisciplineService;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.BL.Services.TeacherService;
using RockSchool.BL.Services.UserService;
using RockSchool.WebApi.Factories;
using RockSchool.WebApi.Helpers;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Attendances;
using RockSchool.WebApi.Models.Subscriptions;
using RockSchool.WebApi.Models.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RockSchool.BL.Models;

namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
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

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var teacher = await _teacherService.GetTeacherByIdAsync(id);

        var result = new TeacherInfo
        {
            TeacherId = teacher.TeacherId,
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

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var teachers = await _teacherService.GetAllTeachersAsync();

        if (teachers.Length == 0) return NotFound();

        return Ok(teachers);
    }

    [HttpGet("{id}/screen-details")]
    public async Task<ActionResult> GetTeacherScreenDetails(Guid id)
    {
        var teacher = await _teacherService.GetTeacherByIdAsync(id);

        var attendanceInfos = new List<ParentAttendanceInfo>();
        var subscriptionInfos = new List<ParentSubscriptionInfo>();

        var allAttendances = await _attendanceService.GetAttendancesByTeacherIdForPeriodOfTime(id, DateTime.MinValue, DateTime.MaxValue);
        if (allAttendances != null)
        {
            attendanceInfos = allAttendances.Where(a => a.GroupId == null).ToParentAttendanceInfos();
            var groupAttendanceInfos = AttendanceBuilder.BuildGroupAttendanceInfos(allAttendances.Where(a => a.GroupId != null));
            attendanceInfos.AddRange(groupAttendanceInfos);
        }

        var subscriptions = await _subscriptionService.GetSubscriptionsByTeacherId(id);
        if (subscriptions != null)
        {
            subscriptionInfos = subscriptions.Where(a => a.GroupId == null).ToParentSubscriptionInfos();
            var groupSubscriptionInfos = SubscriptionBuilder.BuildGroupSubscriptionInfos(subscriptions.Where(a => a.GroupId != null));
            subscriptionInfos.AddRange(groupSubscriptionInfos);
        }

        var teacherScreenDetails = new TeacherScreenDetailsResponse
        {
            Teacher = new TeacherInfo
            {
                TeacherId = teacher.TeacherId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                WorkingPeriods = teacher.WorkingPeriods.ToArray(),
                ScheduledWorkingPeriods = teacher.ScheduledWorkingPeriods.ToArray(),
                Disciplines = teacher.DisciplineIds.ToArray(),
                IsActive = teacher.IsActive,
            },
            Attendances = attendanceInfos.ToArray(),
            Subscriptions = subscriptionInfos.ToArray(),
        };

        return Ok(teacherScreenDetails);
    }

    [HttpGet("available")]
    public async Task<ActionResult> GetAvailableTeachers(int disciplineId, int studentAge, int branchId)
    {
        var teachers = await _teacherService.GetAvailableTeachersAsync(disciplineId, branchId, studentAge);

        var availableTeacherDtos = new List<AvailableTeacherDto>();
        foreach (var teacher in teachers)
        {
            var availableTeacherDto = await BuildAvailableTeacherDto(teacher);
            availableTeacherDtos.Add(availableTeacherDto);
        }

        return Ok(new { availableTeachers = availableTeacherDtos });
    }

    [HttpGet("{id}/workingPeriods")]
    public async Task<ActionResult> GetTeacherWorkingPeriods(Guid id)
    {
        var teacher = await _teacherService.GetTeacherByIdAsync(id);

        var availableTeacherDto = await BuildAvailableTeacherDto(teacher);

        return Ok(new { teacher = availableTeacherDto });
    }

    [HttpPost]
    public async Task<ActionResult> AddTeacher([FromBody] RegisterTeacherRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            throw new Exception("Incorrect requestDto for registration.");
        }

        var newTeacher = new Teacher
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
            AgeLimit = requestDto.Teacher.AgeLimit,
            IsActive = true,
        };

        var teacherId = await _teacherService.AddTeacher(newTeacher);

        return Ok(teacherId);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] TeacherFormDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var teacher = new Teacher
        {
            TeacherId = id,
            FirstName = model.Teacher.FirstName,
            LastName = model.Teacher.LastName,
            BirthDate = model.Teacher.BirthDate,
            Sex = model.Teacher.Sex,
            Phone = model.Teacher.Phone,
            AgeLimit = model.Teacher.AgeLimit,
            AllowGroupLessons = model.Teacher.AllowGroupLessons,
            DisciplineIds = model.Teacher.Disciplines,
            WorkingPeriods = model.WorkingPeriods,
        };

        await _teacherService.UpdateTeacherAsync(teacher, model.DisciplinesChanged, model.PeriodsChanged);

        return Ok();
    }

    [HttpPost("{id}/activate")]
    public async Task<ActionResult> Activate(Guid id)
    {
        await _teacherService.SetTeacherActiveAsync(id, true);

        return Ok();
    }

    [HttpPost("{id}/deactivate")]
    public async Task<ActionResult> Deactivate(Guid id)
    {
        await _teacherService.SetTeacherActiveAsync(id, false);

        return Ok();
    }

    private async Task<AvailableTeacherDto> BuildAvailableTeacherDto(Teacher teacher)
    {


        var allAttendances = await _attendanceService.GetAttendancesByTeacherIdForPeriodOfTime(
            teacher.TeacherId,
            DateTime.MinValue,
            DateTime.MaxValue);

        var attendanceInfos = allAttendances.Where(a => a.GroupId == null).ToParentAttendanceInfos();
        var groupAttendanceInfos = AttendanceBuilder.BuildGroupAttendanceInfos(allAttendances.Where(a => a.GroupId != null));
        attendanceInfos.AddRange(groupAttendanceInfos);

        var availableTeacherDto = new AvailableTeacherDto
        {
            FirstName = teacher.FirstName,
            LastName = teacher.LastName,
            TeacherId = teacher.TeacherId,
            Workload = Random.Shared.Next(1, 100),
            ScheduledWorkingPeriods = teacher.ScheduledWorkingPeriods?
                .Select(swp => new ScheduledWorkingPeriodInfo
                {
                    StartDate = swp.StartDate,
                    EndDate = swp.EndDate,
                    ScheduledWorkingPeriodId = swp.ScheduledWorkingPeriodId,
                    RoomId = swp.RoomId
                })
                .ToList(),

            Attendances = attendanceInfos.ToArray()
        };

        return availableTeacherDto;
    }
}