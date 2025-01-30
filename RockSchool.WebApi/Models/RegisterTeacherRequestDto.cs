using System;
using RockSchool.Data.Entities;

namespace RockSchool.WebApi.Models
{
    public class RegisterTeacherRequestDto
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public long Phone { get; set; }
        public int[] Disciplines { get; set; }
        public WorkingHoursEntity WorkingHoursEntity { get; set; }
    }
}
