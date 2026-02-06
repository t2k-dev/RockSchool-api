using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.DisciplineService;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.BL.Services.TeacherService;
using RockSchool.BL.Services.UserService;
using RockSchool.BL.Teachers;
using RockSchool.BL.Teachers.AddTeacher;
using RockSchool.Domain.Teachers;
using RockSchool.WebApi.Helpers;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]s")]
[ApiController]
public class TeacherController(
    IAddTeacherService addTeacherService,
    ITeacherService teacherService,
    ITeacherScreenDetailsService teacherScreenDetailsService,
    IAttendanceService attendanceService
    ) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var teachers = await teacherService.GetAllTeachersAsync();

        if (teachers.Length == 0) return NotFound();

        return Ok(teachers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var teacher = await teacherService.GetTeacherByIdAsync(id);

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
            AllowBands = teacher.AllowBands,
            AgeLimit = teacher.AgeLimit,
            BranchId = teacher.BranchId,
            WorkingPeriods = teacher.WorkingPeriods?.OrderBy(p => p.WeekDay).ToArray(),
            IsActive = teacher.IsActive,
        };

        return Ok(result);
    }

    [HttpGet("{id}/screen-details")]
    public async Task<ActionResult> GetTeacherScreenDetails(Guid id)
    {
        var details = await teacherScreenDetailsService.Query(id);

        var teacherScreenDetails = new TeacherScreenDetailsResponse
        {
            Teacher = details.Teacher.ToInfo(),
            Attendances = [],
            Subscriptions = [],
        };

        return Ok(teacherScreenDetails);
    }

    [HttpGet("available")]
    public async Task<ActionResult> GetAvailableTeachers(int disciplineId, int studentAge, int branchId)
    {
        var teachers = await teacherService.GetAvailableTeachersAsync(disciplineId, branchId, studentAge);

        var availableTeacherDtos = new List<AvailableTeacherDto>();
        foreach (var teacher in teachers)
        {
            var availableTeacherDto = await BuildAvailableTeacherDto(teacher);
            availableTeacherDtos.Add(availableTeacherDto);
        }

        return Ok(new { availableTeachers = availableTeacherDtos });
    }

    [HttpGet("rehearsable")]
    public async Task<ActionResult> GetRehearsableTeachers(int branchId)
    {
        var teachers = await teacherService.GetRehearsableTeachersAsync(branchId);

        var availableTeacherDtos = new List<AvailableTeacherDto>();
        foreach (var teacher in teachers)
        {
            var availableTeacherDto = await BuildAvailableTeacherDto(teacher);
            availableTeacherDtos.Add(availableTeacherDto);
        }

        return Ok(new { teachers = availableTeacherDtos });
    }

    [HttpGet("{id}/workingPeriods")]
    public async Task<ActionResult> GetTeacherWorkingPeriods(Guid id)
    {
        var teacher = await teacherService.GetTeacherByIdAsync(id);

        var availableTeacherDto = await BuildAvailableTeacherDto(teacher);

        return Ok(new { teacher = availableTeacherDto });
    }

    [HttpPost]
    public async Task<ActionResult> AddTeacher([FromBody] AddTeacherRequest request)
    {
        if (!ModelState.IsValid)
        {
            throw new Exception("Incorrect request for registration.");
        }

        var teacherDto = new TeacherDto
        {
            FirstName = request.Teacher.FirstName,
            LastName = request.Teacher.LastName,
            BirthDate = request.Teacher.BirthDate.ToUniversalTime(),
            Phone = request.Teacher.Phone,
            BranchId = request.Teacher.BranchId,
            Sex = request.Teacher.Sex,
            DisciplineIds = request.Teacher.Disciplines,
            WorkingPeriods = request.WorkingPeriods,
            AllowGroupLessons = request.Teacher.AllowGroupLessons,
            AllowBands = request.Teacher.AllowBands,
            AgeLimit = request.Teacher.AgeLimit,
        };

        var teacherId = await addTeacherService.Handle(teacherDto);

        return Ok(teacherId);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTeacher(Guid id, [FromBody] UpdateTeacherRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var teacherDto = new TeacherDto
        {
            TeacherId = id,
            FirstName = request.Teacher.FirstName,
            LastName = request.Teacher.LastName,
            BirthDate = request.Teacher.BirthDate.ToUniversalTime(),
            Phone = request.Teacher.Phone,
            BranchId = request.Teacher.BranchId,
            Sex = request.Teacher.Sex,
            DisciplineIds = request.Teacher.Disciplines,
            WorkingPeriods = request.WorkingPeriods,
            AllowGroupLessons = request.Teacher.AllowGroupLessons,
            AllowBands = request.Teacher.AllowBands,
            AgeLimit = request.Teacher.AgeLimit,
        };

        await teacherService.UpdateTeacherAsync(teacherDto, request.DisciplinesChanged, request.PeriodsChanged);

        return Ok();
    }

    [HttpPost("{id}/activate")]
    public async Task<ActionResult> Activate(Guid id)
    {
        await teacherService.SetTeacherActiveAsync(id, true);

        return Ok();
    }

    [HttpPost("{id}/deactivate")]
    public async Task<ActionResult> Deactivate(Guid id)
    {
        await teacherService.SetTeacherActiveAsync(id, false);

        return Ok();
    }

    private async Task<AvailableTeacherDto> BuildAvailableTeacherDto(Teacher teacher)
    {


        var allAttendances = await attendanceService.GetAttendancesByTeacherIdForPeriodOfTime(
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