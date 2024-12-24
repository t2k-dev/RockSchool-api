using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Entities
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int WeekDay { get; set; }
        public string StartTime { get; set; }
        public int Duration { get; set; }
        public int DisciplineId { get; set; }
        public Discipline Discipline { get; set; }
    }
}
