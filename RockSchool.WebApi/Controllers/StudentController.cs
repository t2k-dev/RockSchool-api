using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RockSchool.WebApi.Entities;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : MyBaseController
    {
        public StudentController(RockSchoolContext rockSchoolContext, IMapper mapper)
            : base(rockSchoolContext, mapper)
        {
        }

        [EnableCors("MyPolicy")]
        [HttpGet]
        public ActionResult Get()
        {
            var students = _context.Students.ToList();

            if (students == null)
            {
                return NotFound();
            }

            return Ok(students);
        }

        [EnableCors("MyPolicy")]
        [HttpPost]
        public ActionResult Post(AddStudentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newStudent = _mapper.Map<Student>(model);

            _context.Students.Add(newStudent);
            _context.SaveChanges();

            return Ok();
        }

        [EnableCors("MyPolicy")]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]AddStudentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = _context.Students.SingleOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.BirthDate = model.BirthDate;

            _context.SaveChanges();

            return Ok();
        }

        [EnableCors("MyPolicy")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var student = _context.Students.SingleOrDefault(d => d.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            _context.SaveChanges();

            return Ok();
        }
    }
}
