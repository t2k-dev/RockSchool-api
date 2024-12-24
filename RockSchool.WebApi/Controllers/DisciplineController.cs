using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RockSchool.WebApi.Entities;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplineController : MyBaseController
    {
        public DisciplineController(RockSchoolContext rockSchoolContext, IMapper mapper)
            : base(rockSchoolContext, mapper)
        {
        }

        [HttpPost("{disciplineName}")]
        public ActionResult Post(string disciplineName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var discipline = new Discipline()
            {
                DisciplineName = disciplineName,
                IsActive = true
            };

            _context.Disciplines.Add(discipline);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public ActionResult Get()
        {
            var disciplines = _context.Disciplines.ToList();

            var result = _mapper.Map<List<DisciplineDto>>(disciplines);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]DisciplineDto model)
        {
            var discipline = _context.Disciplines.SingleOrDefault(discipline => discipline.Id == id);

            if (discipline == null)
            {
                return NotFound();
            }

            discipline.DisciplineName = model.DisciplineName;
            discipline.IsActive = model.IsActive;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var discipline = _context.Disciplines.SingleOrDefault(d => d.Id == id);
            
            if (discipline == null)
            {
                return NotFound();
            }

            _context.Disciplines.Remove(discipline);
            _context.SaveChanges();

            return Ok();
        }
    }
}
