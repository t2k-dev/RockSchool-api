using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RockSchool.BL.Dtos.Service.Requests.StudentService;
using RockSchool.BL.Services.StudentService;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Students;

namespace RockSchool.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : Controller
{
    private readonly IMapper _mapper;
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService, IMapper mapper)
    {
        _studentService = studentService;
        _mapper = mapper;
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
    public async Task<ActionResult> Get(int id)
    {
        var student = await _studentService.GetById(id);

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

        var studentDto = await _studentService.GetById(id);

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
        if (!ModelState.IsValid) throw new Exception("Incorrect requestDto for registration.");

        var newStudent = new AddStudentServiceRequestDto
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            BirthDate = requestDto.BirthDate.ToUniversalTime(),
            Sex = requestDto.Sex,
            Phone = requestDto.Phone,
            Level = requestDto.Level,
        };

        var id = await _studentService.AddStudentAsync(newStudent);

        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateStudentRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updateStudentDto = _mapper.Map<UpdateStudentServiceRequestDto>(requestDto);
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