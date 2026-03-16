using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Attendances.Rescheduling;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.AttendeeService;
using RockSchool.BL.Subscriptions.Trial;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Subscriptions;
using System;
using System.Threading.Tasks;


namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]")]
[ApiController]
public class AttendanceController(
    IAttendanceService attendanceService,
    IReschedulingService reschedulingService,
    ITrialSubscriptionService trialSubscriptionService,
    IAttendeeService attendeeService
    )
    : Controller
{
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var attendances = await attendanceService.GetAllAttendancesAsync();

        return Ok(attendances);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var attendance = await attendanceService.GetAttendanceAsync(id);
        return Ok(attendance);
    }

    [HttpPost("{id}/declineTrial")]
    public async Task<ActionResult> DeclineTrial(Guid id, SubmitAttendanceRequest request)
    {
        await trialSubscriptionService.DeclineTrial(id, request.SubscriptionId, request.StatusReason, request.Comment);

        return Ok();
    }

    [HttpPost("{id}/acceptTrial")]
    public async Task<ActionResult> AcceptTrial(Guid id, SubmitAttendanceRequest request)
    {
        await trialSubscriptionService.AcceptTrial(id, request.SubscriptionId, request.StatusReason, request.Comment);

        return Ok();
    }

    [HttpPut("{id}/submitAttendee")]
    public async Task<ActionResult> UpdateStatus(Guid id, SubmitAttendeeRequest request)
    {
        var updated = await attendeeService.UpdateStatus(id, request.AttendeeId, request.AttendeeStatus);
        if (!updated)
            return NotFound();

        return Ok();
    }

    [HttpPost("{id}/reschedule")]
    public async Task<ActionResult> RescheduleAttendance(Guid id, RescheduleAttendanceRequest request)
    {
        var attendance = await reschedulingService.RescheduleAttendanceByAdmin(id, request.NewStartDate, request.NewEndDate, request.RoomId, request.StatusReason);

        return Ok(attendance);
    }
}