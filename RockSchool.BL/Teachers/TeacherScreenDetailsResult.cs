using RockSchool.BL.Models.Dtos;
using RockSchool.Domain.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Teachers
{
    public class TeacherScreenDetailsResult
    {
        public Teacher Teacher { get; set; } = null!;
        public AttendanceWithAttendeesDto[] Attendances { get; set; } = [];
        public Subscription[] Subscriptions { get; set; } = [];
        public Band[] Bands { get; set; } = [];
    }
}
