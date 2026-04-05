using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.TariffService;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers;

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
        var tariffs = await _tariffService.GetAllTariffsAsync();
        return Ok(tariffs);
    }

    [HttpGet("current")]
    public async Task<ActionResult> GetCurrentTariffs()
    {
        var tariffs = await _tariffService.GetTariffsAsync();
        return Ok(tariffs);
    }

    [HttpGet("{subscriptionType}/single")]
    public async Task<ActionResult> GetTariff(int subscriptionType, int? disciplineId)
    {
        var trialTariff = await _tariffService.GetTariffAsync((SubscriptionType)subscriptionType, disciplineId);
        if (trialTariff == null)
            return NotFound(new ErrorResponse("Trial tariff not found"));
        return Ok(trialTariff);
    }

    [HttpGet("{subscriptionType}")]
    public async Task<ActionResult> GetTariffs(int subscriptionType)
    {
        var trialTariff = await _tariffService.GetTariffsAsync((SubscriptionType)subscriptionType);
        if (trialTariff == null)
            return NotFound(new ErrorResponse("Trial tariff not found"));
        return Ok(trialTariff);
    }

    [HttpGet("trial")]
    public async Task<ActionResult> GetTrialTariff()
    {
        var trialTariff = await _tariffService.GetTrialTariffAsync();
        if (trialTariff == null)
            return NotFound(new ErrorResponse("Trial tariff not found"));
        return Ok(trialTariff);
    }

    [HttpPost]
    public async Task<ActionResult> AddTariff([FromBody] AddTariffDto requestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newTariff = Tariff.Create(
            requestDto.Amount,
            requestDto.StartDate.ToUniversalTime(),
            requestDto.EndDate.ToUniversalTime(),
            requestDto.AttendanceLength,
            requestDto.AttendanceCount,
            requestDto.SubscriptionType,
            requestDto.DisciplineId);

        var id = await _tariffService.AddTariffAsync(newTariff);

        return Ok(id);
    }
}
