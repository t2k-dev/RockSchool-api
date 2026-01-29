using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.BandService;
using RockSchool.BL.Services.BandStudentService;
using RockSchool.WebApi.Helpers;
using RockSchool.WebApi.Models.Bands;
using System;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]")]
[ApiController]
public class BandController(
    IBandService bandService,
    IBandStudentService bandStudentService)
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

    [HttpGet("teacher/{teacherId}")]
    public async Task<ActionResult> GetBandsByTeacher(Guid teacherId)
    {
        var bands = await bandService.GetByTeacherIdAsync(teacherId);
        var result = bands.ToInfos();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateBand([FromBody] CreateBandRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var bandId = await bandService.AddBandAsync(request.Name, request.TeacherId, request.Members, request.Schedules);
        
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBand(Guid id, [FromBody] CreateBandRequest dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        /*var band = dto.ToModel();
        band.BandId = id;
        
        await bandService.UpdateBandAsync(band);
        */
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBand(Guid id)
    {
        await bandService.DeleteBandAsync(id);
        return NoContent();
    }

    [HttpPost("add-student")]
    public async Task<ActionResult> AddStudentToBand([FromBody] AddStudentToBandDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var bandStudent = dto.ToModel();
        var bandStudentId = await bandStudentService.AddStudentToBandAsync(bandStudent);
        
        return Ok(bandStudentId);
    }

    [HttpDelete("{bandId}/students/{studentId}")]
    public async Task<ActionResult> RemoveStudentFromBand(Guid bandId, Guid studentId)
    {
        await bandStudentService.RemoveStudentFromBandAsync(bandId, studentId);
        return NoContent();
    }

    [HttpGet("{id}/students")]
    public async Task<ActionResult> GetBandStudents(Guid id)
    {
        var bandStudents = await bandStudentService.GetByBandIdAsync(id);
        var result = bandStudents.ToInfos();
        return Ok(result);
    }
}