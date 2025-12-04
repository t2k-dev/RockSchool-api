using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.Data.Enums;
using RockSchool.WebApi.Helpers;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Attendances;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RockSchool.BL.Models;

namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]")]
[ApiController]
public class AttendanceController : Controller
{
    private readonly IAttendanceService _attendanceService;
    private readonly ISubscriptionService _subscriptionService;

    public AttendanceController(IAttendanceService attendanceService, ISubscriptionService subscriptionService)
    {
        _attendanceService = attendanceService;
        _subscriptionService = subscriptionService;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var attendances = await _attendanceService.GetAllAttendancesAsync();

        return Ok(attendances);
    }

    [EnableCors("MyPolicy")]
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var attendance = await _attendanceService.GetAttendanceAsync(id);
        return Ok(attendance);
    }

    [HttpPost("addLessons")]
    public async Task<ActionResult> AddForStudent(AddAttendancesDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var addAttendanceForStudentServiceDto = new Attendance()
        {
            StudentId = dto.StudentId,
            TeacherId = dto.TeacherId,
            DisciplineId = dto.DisciplineId,
            StartDate = dto.StartingDate
        };

        //await _attendanceService.AddAttendancesToStudent(addAttendanceForStudentServiceDto);

        return Ok();
    }

    [HttpPost("{id}/declineTrial")]
    public async Task<ActionResult> DeclineTrial(Guid id, DeclineAttendanceRequest declineAttendanceRequest)
    {
        var attendance = await _attendanceService.GetAttendanceAsync(id);
        attendance.Status = AttendanceStatus.Attended;
        attendance.StatusReason = declineAttendanceRequest.StatusReason;

        await _attendanceService.UpdateAttendanceAsync(attendance);

        await _subscriptionService.DeclineTrialSubscription(attendance.SubscriptionId, declineAttendanceRequest.StatusReason);

        return Ok();
    }

    [HttpPost("{id}/acceptTrial")]
    public async Task<ActionResult> AcceptTrial(Guid id, DeclineAttendanceRequest declineAttendanceRequest)
    {
        var attendance = await _attendanceService.GetAttendanceAsync(id);
        attendance.Status = AttendanceStatus.Attended;
        attendance.StatusReason = declineAttendanceRequest.StatusReason;

        await _attendanceService.UpdateAttendanceAsync(attendance);

        await _subscriptionService.AcceptTrialSubscription(attendance.SubscriptionId, declineAttendanceRequest.StatusReason);

        return Ok();
    }

    [HttpPost("{id}/attend")]
    public async Task<ActionResult> Attend(Guid id, DeclineAttendanceRequest declineAttendanceRequest)
    {
        var attendance = await _attendanceService.GetAttendanceAsync(id);
        attendance.Status = AttendanceStatus.Attended;
        attendance.StatusReason = declineAttendanceRequest.StatusReason;

        await _attendanceService.UpdateAttendanceAsync(attendance);

        return Ok();
    }

    [HttpPost("{id}/missed")]
    public async Task<ActionResult> Missed(Guid id, DeclineAttendanceRequest declineAttendanceRequest)
    {
        var attendance = await _attendanceService.GetAttendanceAsync(id);
        attendance.Status = AttendanceStatus.Missed;
        attendance.StatusReason = declineAttendanceRequest.StatusReason;

        await _attendanceService.UpdateAttendanceAsync(attendance);

        return Ok();
    }

    [HttpPost("submit")]
    public async Task<ActionResult> Submit(SubmitGroupAttendanceRequest submitGroupAttendanceRequest)
    {
        var attendances = submitGroupAttendanceRequest.ChildAttendances.ToModels();
        await _attendanceService.UpdateAttendances(attendances);

        return Ok();
    }

    [HttpPost("{id}/updateStatus/{status}")]
    public async Task<ActionResult> UpdateStatus(Guid id, int status)
    {
        await _attendanceService.UpdateStatusAsync(id, status);
        return Ok();
    }
}