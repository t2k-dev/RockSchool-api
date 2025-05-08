using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers;

[EnableCors("MyPolicy")]
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

    [EnableCors("MyPolicy")]
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var attendance = await _attendanceService.GetAttendanceAsync(id);
        return Ok(attendance);
    }

    [HttpPost("addLessons")]
    public async Task<ActionResult> AddForStudent(AddAttendancesDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var addAttendanceForStudentServiceDto = new AttendanceDto()
        {
            StudentId = dto.StudentId,
            TeacherId = dto.TeacherId,
            DisciplineId = dto.DisciplineId,
            NumberOfAttendances = dto.NumberOfAttendances,
            StartDate = dto.StartingDate
        };

        //await _attendanceService.AddAttendancesToStudent(addAttendanceForStudentServiceDto);

        return Ok();
    }

    
    [HttpPost("{id}/updateStatus/{status}")]
    public async Task<ActionResult> UpdateStatus(Guid id, int status)
    {
        await _attendanceService.UpdateStatusAsync(id, status);
        return Ok();
    }
}