using System;
using RockSchool.WebApi.Entities;

namespace RockSchool.WebApi.Models
{
    public class RegisterTeacherDto
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public long Phone { get; set; }
        public int[] Disciplines { get; set; }
        public WorkingHours WorkingHours { get; set; }
    }
}
