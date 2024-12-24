using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RockSchool.WebApi.Entities;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Services
{
    public class StudentService
    {
        protected readonly RockSchoolContext _context;
        protected readonly IMapper _mapper;

        public StudentService(RockSchoolContext rockSchoolContext, IMapper mapper)
        {
            _context = rockSchoolContext;
            _mapper = mapper;
        }

        public void AddStudent(AddStudentDto model)
        {
            var newStudent = _mapper.Map<Student>(model);

            _context.Students.Add(newStudent);
            _context.SaveChanges();
        }
    }
}
