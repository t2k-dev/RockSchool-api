using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos.Service.Requests.AttendanceService;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendanceController : Controller
{
    private readonly IAttendanceService _attendanceService;

    public AttendanceController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var attendances = await _attendanceService.GetAllAttendancesAsync();

        return Ok(attendances);
    }

    [HttpPost("addLessons")]
    public async Task<ActionResult> AddForStudent(AddAttendancesDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var addAttendanceForStudentServiceDto = new AddAttendanceServiceRequestDto
        {
            StudentId = dto.StudentId,
            TeacherId = dto.TeacherId,
            DisciplineId = dto.DisciplineId,
            NumberOfAttendances = dto.NumberOfAttendances,
            StartingDate = dto.StartingDate
        };

        await _attendanceService.AddAttendancesToStudent(addAttendanceForStudentServiceDto);

        return Ok();
    }
}