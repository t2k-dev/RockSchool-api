using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos.Service.Requests.StudentService;
using RockSchool.BL.Services.StudentService;
using RockSchool.WebApi.Models;

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

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var studentsDto = await _studentService.GetAllStudentsAsync();

        if (studentsDto?.Length == 0)
            return NotFound();

        return Ok(studentsDto);
    }

    // TODO: We will use this method
    // [HttpPost]
    // public ActionResult Post(AddStudentDto model)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //
    //     var newStudent = _mapper.Map<StudentEntity>(model);
    //
    //     _context.Students.Add(newStudent);
    //     _context.SaveChanges();
    //
    //     return Ok();
    // }

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
    public async Task<ActionResult> Delete(int id)
    {
        await _studentService.DeleteStudentAsync(id);

        return Ok();
    }
}