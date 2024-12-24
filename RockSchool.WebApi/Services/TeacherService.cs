using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RockSchool.WebApi.Entities;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Services
{
    public class TeacherService
    {
        protected readonly RockSchoolContext _context;
        protected readonly IMapper _mapper;

        public TeacherService(RockSchoolContext rockSchoolContext, IMapper mapper)
        {
            _context = rockSchoolContext;
            _mapper = mapper;
        }

        public int AddTeacher(AddTeacherDto model)
        {
            var newTeacher = new Teacher()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                BirthDate = model.BirthDate,
                UserId = model.UserId,
                WorkingHours = model.WorkingHours,
                Disciplines = new List<Discipline>()
            };

            foreach (var disciplineId in model.Disciplines)
            {
                var discipline = _context.Disciplines.SingleOrDefault(d => d.Id == disciplineId);
                newTeacher.Disciplines.Add(discipline);
            }

            _context.Teachers.Add(newTeacher);
            _context.SaveChanges();

            return newTeacher.TeacherId;
        }
    }

}
