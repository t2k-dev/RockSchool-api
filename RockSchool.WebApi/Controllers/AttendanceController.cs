using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.Domain.Enums;
using RockSchool.WebApi.Helpers;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Attendances;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RockSchool.Domain.Entities;


namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]")]
[ApiController]
public class AttendanceController(
    IAttendanceService attendanceService,
    ISubscriptionService subscriptionService,
    IAttendanceSubmitService attendanceSubmitService)
    : Controller
{
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var attendances = await attendanceService.GetAllAttendancesAsync();

        return Ok(attendances);
    }

    [EnableCors("MyPolicy")]
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var attendance = await attendanceService.GetAttendanceAsync(id);
        return Ok(attendance);
    }

    [HttpPost("{id}/declineTrial")]
    public async Task<ActionResult> DeclineTrial(Guid id, SubmitAttendanceRequest request)
    {
        await attendanceSubmitService.DeclineTrial(id, request.StatusReason, request.Comment);

        return Ok();
    }

    [HttpPost("{id}/acceptTrial")]
    public async Task<ActionResult> AcceptTrial(Guid id, SubmitAttendanceRequest request)
    {
        await attendanceSubmitService.AcceptTrial(id, request.StatusReason, request.Comment);

        return Ok();
    }

    [HttpPost("{id}/missedTrial")]
    public async Task<ActionResult> MissedTrial(Guid id, SubmitAttendanceRequest request)
    {
        await attendanceSubmitService.MissedTrial(id, request.StatusReason, request.Comment);

        return Ok();
    }

    [HttpPost("{id}/submit")]
    public async Task<ActionResult> Submit(Guid id, AttendanceInfo attendanceInfo)
    {
        await attendanceSubmitService.SubmitAttendance(id, attendanceInfo.Status, attendanceInfo.StatusReason, attendanceInfo.Comment);

        return Ok();
    }

    [HttpPost("submit")]
    public async Task<ActionResult> SubmitGroup(SubmitGroupAttendanceRequest request)
    {
        var attendanceInfos = request.ChildAttendances;
        foreach (var attendanceInfo in attendanceInfos)
        {
            await attendanceSubmitService.SubmitAttendance(attendanceInfo.AttendanceId, attendanceInfo.Status, attendanceInfo.StatusReason, request.Comment);
        }

        return Ok();
    }

    [HttpPut("{id}/comment/{comment}")]
    public async Task<ActionResult> UpdateComment(Guid id, string comment)
    {
        // TODO: fix routing
        //await attendanceService.UpdateCommentAsync(id, comment);

        return Ok();
    }

    [HttpPost("{id}/attend")]
    public async Task<ActionResult> Attend(Guid id, SubmitAttendanceRequest declineAttendanceRequest)
    {
        var attendance = await attendanceService.GetAttendanceAsync(id);

        attendance.MarkAsAttended(declineAttendanceRequest.StatusReason);

        //await attendanceService.UpdateAttendanceAsync(attendance);

        return Ok();
    }

    [HttpPost("{id}/missed")]
    public async Task<ActionResult> Missed(Guid id, SubmitAttendanceRequest declineAttendanceRequest)
    {
        var attendance = await attendanceService.GetAttendanceAsync(id);

        attendance.MarkAsMissed(declineAttendanceRequest.StatusReason);

        //await attendanceService.UpdateAttendanceAsync(attendance);

        return Ok();
    }
}