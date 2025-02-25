using System.Collections.Generic;
using RockSchool.BL.Dtos;

namespace RockSchool.WebApi.Models.Students
{
    public class StudentScreenDetailsDto
    {
        public StudentDto Student { get; set; }
        public List<string> Subscriptions { get; set; }
    }
}
