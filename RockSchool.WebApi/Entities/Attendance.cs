using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Entities
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public DateTime BeginDate { get; set; }
        public int Status { get; set; }
        public int? RoomId { get; set; }
        public Room Room { get; set; }
        public int Duration { get; set; }
        public string Comment { get; set; }
    }
}
