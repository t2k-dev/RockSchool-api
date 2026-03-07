using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.BandService;
using RockSchool.BL.Services.BandMemberService;
using RockSchool.WebApi.Helpers;
using RockSchool.WebApi.Models.Bands;
using System;
using System.Threading.Tasks;
using RockSchool.BL.Models;

namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]")]
[ApiController]
public class BandController(
    IBandService bandService,
    IBandMemberService bandMemberService)
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

    [HttpPost("add-student")]
    public async Task<ActionResult> AddStudentToBand([FromBody] AddStudentToBandDto dto)
    {
        throw new NotImplementedException();
        /*if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var bandMember = dto.ToModel();
        var bandMemberId = await bandMemberService.AddStudentToBandAsync(bandMember);

        return Ok(bandMemberId);*/
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