using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
    private readonly IAttendanceSubmitService _attendanceSubmitService ;
    private readonly ISubscriptionService _subscriptionService;

    public AttendanceController(IAttendanceService attendanceService, ISubscriptionService subscriptionService, IAttendanceSubmitService attendanceSubmitService)
    {
        _attendanceService = attendanceService;
        _subscriptionService = subscriptionService;
        _attendanceSubmitService = attendanceSubmitService;
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
    public async Task<ActionResult> DeclineTrial(Guid id, SubmitAttendanceRequest request)
    {
        await _attendanceSubmitService.DeclineTrial(id, request.StatusReason, request.Comment);

        return Ok();
    }

    [HttpPost("{id}/acceptTrial")]
    public async Task<ActionResult> AcceptTrial(Guid id, SubmitAttendanceRequest request)
    {
        await _attendanceSubmitService.AcceptTrial(id, request.StatusReason, request.Comment);

        return Ok();
    }

    [HttpPost("{id}/missedTrial")]
    public async Task<ActionResult> MissedTrial(Guid id, SubmitAttendanceRequest request)
    {
        await _attendanceSubmitService.MissedTrial(id, request.StatusReason, request.Comment);

        return Ok();
    }

    [HttpPost("{id}/submit")]
    public async Task<ActionResult> Submit(Guid id, AttendanceInfo attendanceInfo)
    {
        await _attendanceSubmitService.SubmitAttendance(id, attendanceInfo.Status, attendanceInfo.StatusReason, attendanceInfo.Comment);

        return Ok();
    }

    [HttpPost("submit")]
    public async Task<ActionResult> SubmitGroup(SubmitGroupAttendanceRequest request)
    {
        var attendanceInfos = request.ChildAttendances;
        foreach (var attendanceInfo in attendanceInfos)
        {
            await _attendanceSubmitService.SubmitAttendance(attendanceInfo.AttendanceId, attendanceInfo.Status, attendanceInfo.StatusReason, request.Comment);
        }

        return Ok();
    }

    [HttpPut("{id}/comment/{comment}")]
    public async Task<ActionResult> UpdateComment(Guid id, string comment)
    {
        // TODO: fix routing
        await _attendanceService.UpdateCommentAsync(id, comment);

        return Ok();
    }

    [HttpPost("{id}/attend")]
    public async Task<ActionResult> Attend(Guid id, SubmitAttendanceRequest declineAttendanceRequest)
    {
        var attendance = await _attendanceService.GetAttendanceAsync(id);
        attendance.Status = AttendanceStatus.Attended;
        attendance.StatusReason = declineAttendanceRequest.StatusReason;

        await _attendanceService.UpdateAttendanceAsync(attendance);

        return Ok();
    }

    [HttpPost("{id}/missed")]
    public async Task<ActionResult> Missed(Guid id, SubmitAttendanceRequest declineAttendanceRequest)
    {
        var attendance = await _attendanceService.GetAttendanceAsync(id);
        attendance.Status = AttendanceStatus.Missed;
        attendance.StatusReason = declineAttendanceRequest.StatusReason;

        await _attendanceService.UpdateAttendanceAsync(attendance);

        return Ok();
    }

    [HttpPost("{id}/updateStatus/{status}")]
    public async Task<ActionResult> UpdateStatus(Guid id, int status)
    {
        await _attendanceService.UpdateStatusAsync(id, status);
        return Ok();
    }
}