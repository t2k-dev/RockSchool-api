using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos;
using RockSchool.BL.Services.ScheduleService;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController : Controller
{
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var schedules = await _scheduleService.GetAllSchedulesAsync();

        return Ok(schedules);
    }

    [HttpPost]
    public async Task<ActionResult> Post(AddScheduleDto requestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        
        // var serviceRequestDto = new ScheduleDto()
        // {
        //     StudentId = requestDto.StudentId,
        //     WeekDay = requestDto.WeekDay,
        //     StartTime = requestDto.StartTime,
        //     Duration = requestDto.Duration,
        //     DisciplineId = requestDto.DisciplineId
        // };

        // await _scheduleService.AddScheduleAsync(serviceRequestDto);

        return Ok();
    }
}