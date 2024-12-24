using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Entities
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public long Phone { get; set; }
        public ICollection<Discipline> Disciplines { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        [Column(TypeName ="jsonb")]
        public WorkingHours WorkingHours { get; set; }
    }

    // Json objects
    public class WorkingHours
    {
        public WorkingPeriod[] WorkingPeriods { get; set; }
        public WorkingPeriod[] Breaks { get; set; }
    }

    public class WorkingPeriod
    {
        public int Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
