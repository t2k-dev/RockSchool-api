using RockSchool.BL.Models.Dtos;
using RockSchool.Domain.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockSchool.BL.Teachers
{
    public class TeacherScreenDetailsResult
    {
        public Teacher Teacher { get; set; }
        public AttendanceWithAttendeesDto[] Attendances { get; set; }
    }
}
