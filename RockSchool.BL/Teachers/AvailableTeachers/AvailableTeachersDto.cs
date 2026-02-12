using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Teachers;

namespace RockSchool.BL.Teachers.AvailableTeachers
{
    public class AvailableTeachersDto
    {
        public Teacher Teacher { get; set; }
        public Attendance[] Attendances { get; set; }
        public int Workload { get; set; }
    }
}
