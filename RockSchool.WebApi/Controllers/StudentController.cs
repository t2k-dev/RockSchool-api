using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos;
using RockSchool.BL.Services.BranchService;
using RockSchool.BL.Services.StudentService;
using RockSchool.Data.Enums;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Students;

namespace RockSchool.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : Controller
{
    private readonly IStudentService _studentService;
    private readonly IBranchService _branchService;

    public StudentController(IStudentService studentService, IBranchService branchService)
    {
        _studentService = studentService;
        _branchService = branchService;
    }

    [EnableCors("MyPolicy")]
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var studentsDto = await _studentService.GetAllStudentsAsync();

        if (studentsDto?.Length == 0)
            return NotFound();

        return Ok(studentsDto);
    }

    [EnableCors("MyPolicy")]
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var student = await _studentService.GetByIdAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }


    [EnableCors("MyPolicy")]
    [HttpGet("getStudentScreenDetails/{id}")]
    public async Task<ActionResult> GetStudentScreenDetails(Guid id)
    {
        var studentDto = await _studentService.GetByIdAsync(id);

        var studentScreenDetailsDto = new StudentScreenDetailsDto
        {
            Student = studentDto,
            Subscriptions = new List<string>(),
        };

        return Ok(studentScreenDetailsDto);
    }

    [EnableCors("MyPolicy")]
    [HttpPost("addStudent")]
    public async Task<ActionResult> AddStudent([FromBody] RegisterStudentRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            throw new Exception("Incorrect requestDto for registration.");

        var newStudent = new StudentDto()
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            BirthDate = requestDto.BirthDate.ToUniversalTime(),
            Sex = requestDto.Sex,
            Phone = requestDto.Phone,
            Level = requestDto.Level,
            BranchId = requestDto.BranchId
        };

        var id = await _studentService.AddStudentAsync(newStudent);

        return Ok(id);
    }

    [EnableCors("MyPolicy")]
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, UpdateStudentRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updateStudentDto = new StudentDto()
        {
            StudentId = id,
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            BirthDate = requestDto.BirthDate,
            Sex = requestDto.Sex,
            Phone = requestDto.Phone,
            Level = requestDto.Level
        };
        await _studentService.UpdateStudentAsync(updateStudentDto);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _studentService.DeleteStudentAsync(id);

        return Ok();
    }
}