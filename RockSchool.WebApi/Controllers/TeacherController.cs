using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RockSchool.WebApi.Entities;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : MyBaseController
    {
        public TeacherController(RockSchoolContext rockSchoolContext, IMapper mapper)
            : base(rockSchoolContext, mapper)
        {
        }

        [EnableCors("MyPolicy")]
        [HttpGet]
        public ActionResult Get()
        {
            var teachers = _context.Teachers
                .Include(t => t.Disciplines)
                .ToList();

            if (teachers == null)
            {
                return NotFound();
            }

            return Ok(teachers);
        }

        [EnableCors("MyPolicy")]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var teacher = _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Disciplines)
                .SingleOrDefault(t => t.TeacherId == id);

            if (teacher == null)
            {
                return NotFound();
            }

            var result = new TeacherDto
            {
                Email = teacher.User.Login,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                MiddleName = teacher.MiddleName,
                BirthDate = teacher.BirthDate,
                Phone = teacher.Phone,
                Disciplines = teacher.Disciplines.Select(d => d.Id).ToArray(),
            };

            return Ok(result);
        }

        [EnableCors("MyPolicy")]
        [HttpPost]
        public ActionResult Post(AddTeacherDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newTeacher = new Teacher()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                BirthDate = model.BirthDate,
                UserId = model.UserId,
                Disciplines = new List<Discipline>()
            };

            foreach (var disciplineId in model.Disciplines)
            {
                var discipline = _context.Disciplines.SingleOrDefault(d => d.Id == disciplineId);
                newTeacher.Disciplines.Add(discipline);
            }

            _context.Teachers.Add(newTeacher);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] AddTeacherDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacher = _context.Teachers
                .Include(t => t.Disciplines)
                .SingleOrDefault(s => s.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            teacher.FirstName = model.FirstName;
            teacher.LastName = model.LastName;
            teacher.MiddleName = model.MiddleName;
            teacher.BirthDate = model.BirthDate;

            teacher.Disciplines.Clear();

            foreach (var disciplineId in model.Disciplines)
            {
                var discipline = _context.Disciplines.SingleOrDefault(d => d.Id == disciplineId);
                teacher.Disciplines.Add(discipline);
            }

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var teacher = _context.Teachers.SingleOrDefault(d => d.TeacherId == id);

            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            _context.SaveChanges();

            return Ok();
        }
    }
}
