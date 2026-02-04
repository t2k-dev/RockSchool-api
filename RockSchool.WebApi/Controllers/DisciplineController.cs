using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Models;
using RockSchool.BL.Services.DisciplineService;
using RockSchool.Domain.Entities;
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

        await _disciplineService.AddDisciplineAsync(disciplineName);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var disciplines = await _disciplineService.GetAllDisciplinesAsync();

        return Ok(disciplines);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _disciplineService.DeleteDisciplineAsync(id);

        return Ok();
    }
}