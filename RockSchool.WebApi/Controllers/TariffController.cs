using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Models;
using RockSchool.BL.Services.TariffService;
using RockSchool.Data.Enums;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]")]
[ApiController]
public class TariffController : ControllerBase
{
    private readonly ITariffService _tariffService;

    public TariffController(ITariffService tariffService)
    {
        _tariffService = tariffService;
    }

    [HttpGet]
    public async Task<ActionResult> GetTariffs()
    {
        try
        {
            var tariffs = await _tariffService.GetAllTariffsAsync();
            return Ok(tariffs);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{subscriptionType}/single")]
    public async Task<ActionResult> GetTariff(int subscriptionType, int? disciplineId)
    {
        try
        {
            var trialTariff = await _tariffService.GetTariffAsync((SubscriptionType)subscriptionType, disciplineId);
            if (trialTariff == null)
            {
                return NotFound(new { message = "Trial tariff not found" });
            }
            return Ok(trialTariff);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{subscriptionType}")]
    public async Task<ActionResult> GetTariffs(int subscriptionType)
    {
        try
        {
            var trialTariff = await _tariffService.GetTariffsAsync((SubscriptionType)subscriptionType);
            if (trialTariff == null)
            {
                return NotFound(new { message = "Trial tariff not found" });
            }
            return Ok(trialTariff);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("trial")]
    public async Task<ActionResult> GetTrialTariff()
    {
        try
        {
            var trialTariff = await _tariffService.GetTrialTariffAsync();
            if (trialTariff == null)
            {
                return NotFound(new { message = "Trial tariff not found" });
            }
            return Ok(trialTariff);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddTariff([FromBody] AddTariffDto requestDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newTariff = new Tariff
            {
                TariffId = Guid.NewGuid(),
                Amount = requestDto.Amount,
                AttendanceCount = requestDto.AttendanceCount,
                AttendanceLength = requestDto.AttendanceLength,
                DisciplineId = requestDto.DisciplineId,
                EndDate = requestDto.EndDate.ToUniversalTime(),
                StartDate = requestDto.StartDate.ToUniversalTime(),
                SubscriptionType = requestDto.SubscriptionType
            };

            var id = await _tariffService.AddTariffAsync(newTariff);

            return Ok(id);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}