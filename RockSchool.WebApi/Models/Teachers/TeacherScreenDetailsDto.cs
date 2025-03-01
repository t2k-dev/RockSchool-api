using System.Collections.Generic;
using RockSchool.BL.Dtos;

namespace RockSchool.WebApi.Models.Teachers
{
    public class TeacherScreenDetailsDto
    {
        public TeacherDto Teacher { get; set; }
        public List<string> Subscriptions { get; set; }
    }
}
