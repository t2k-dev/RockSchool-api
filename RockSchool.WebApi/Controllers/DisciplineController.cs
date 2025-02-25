using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.DisciplineService;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DisciplineController : Controller
{
    private readonly IDisciplineService _disciplineService;

    public DisciplineController(IDisciplineService disciplineService)
    {
        _disciplineService = disciplineService;
    }


    [HttpPost("{disciplineName}")]
    public async Task<ActionResult> Post(string disciplineName)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var addDisciplineServiceDto = new BL.Dtos.DisciplineDto()
        {
            Name = disciplineName,
            IsActive = true
        };

        await _disciplineService.AddDisciplineAsync(addDisciplineServiceDto);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var disciplines = await _disciplineService.GetAllDisciplinesAsync();

        return Ok(disciplines);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] DisciplineDto requestDto)
    {
        var updateDisciplineServiceDto = new BL.Dtos.DisciplineDto()
        {
            Name = requestDto.DisciplineName,
            IsActive = requestDto.IsActive
        };

        await _disciplineService.UpdateDisciplineAsync(updateDisciplineServiceDto);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _disciplineService.DeleteDisciplineAsync(id);

        return Ok();
    }
}