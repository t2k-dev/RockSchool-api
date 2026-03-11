using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Bands;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.WebApi.Helpers;
using RockSchool.WebApi.Models.Bands;
using RockSchool.WebApi.Models.Subscriptions;
using System;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]")]
[ApiController]
public class BandController(
    IBandService bandService,
    IBandMemberService bandMemberService,
    IBandFormDataService bandFormDataService,
    IReschedulingService reschedulingService)
    : Controller
{
    [HttpGet]
    public async Task<ActionResult> GetAllBands()
    {
        var bands = await bandService.GetAllAsync();
        var result = bands.ToInfos();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetBand(Guid id)
    {
        var band = await bandService.GetByIdAsync(id);

        if (band == null)
            return NotFound();

        var result = band.ToInfo();
        return Ok(result);
    }

    [HttpGet("{id}/screen-details")]
    public async Task<ActionResult> GetBandScreenDetails(Guid id)
    {
        var band = await bandService.GetByIdAsync(id);

        var result = new BandScreenDetailsDto
        {
            Band = band.ToInfo(),
        };
        return Ok(result);
    }

    [HttpGet("{id}/form-data")]
    public async Task<ActionResult> GetBandFormData(Guid id)
    {
        var result = await bandFormDataService.Query(id);

        var response = new
        {
            Band = result.Band.ToInfo(),
            ScheduleSlots = result.ScheduleSlots.ToInfos(),
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> AddBand([FromBody] CreateBandRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var scheduleSlots = request.ScheduleSlots?.ToDto() ?? [];

        var bandId = await bandService.AddBandAsync(request.Name, request.TeacherId, request.Members, scheduleSlots);
        
        return Ok(bandId);
    }

    [HttpPut("{id}/activate")]
    public async Task<ActionResult> ActivateBand(Guid id)
    {
        var band = await bandService.ActivateBandAsync(id);
        if (band == null)
            return NotFound();
        return NoContent();
    }

    [HttpPut("{id}/deactivate")]
    public async Task<ActionResult> DeactivateBand(Guid id)
    {
        var band = await bandService.DeactivateBandAsync(id);
        if (band == null)
            return NotFound();
        return NoContent();
    }

    [HttpPut("{id}/schedules")]
    public async Task<ActionResult> UpdateSchedules(Guid id, [FromBody] UpdateSchedulesRequest request)
    {
        var newSchedules = request.Schedules.ToDto();
        await reschedulingService.UpdateScheduleByBand(id, DateTime.Now, newSchedules);

        return Ok();
    }

    [HttpPost("{id}/add-member")]
    public async Task<ActionResult> AddMemberToBand(Guid id,[FromBody] AddMemberToBandDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await bandService.AddBandMemberAsync(id, dto.StudentId, null);

        return Ok();
    }

    [HttpPost("{id}/generate-attendances")]
    public async Task<ActionResult> GenerateAttendances(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await bandService.CreateAttendances(id, DateTime.Now);

        return Ok();
    }

    [HttpDelete("{bandId}/students/{bandMemberId}")]
    public async Task<ActionResult> RemoveStudentFromBand(Guid bandMemberId)
    {
        await bandMemberService.DeleteBandMemberAsync(bandMemberId);
        return NoContent();
    }

    [HttpGet("{id}/students")]
    public async Task<ActionResult> GetBandMembers(Guid id)
    {
        var bandMembers = await bandMemberService.GetByBandIdAsync(id);
        var result = bandMembers.ToInfos();
        return Ok(result);
    }
}