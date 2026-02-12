using RockSchool.WebApi.Models.Attendances;
using System;
using System.Collections.Generic;

namespace RockSchool.WebApi.Models.Teachers
{
    public class AvailableTeacherResponse
    {
        public Guid TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Workload { get; set; }
        public AttendanceInfo[] Attendances { get; set; }
        public List<ScheduledWorkingPeriodInfo> ScheduledWorkingPeriods { get; set; }
    }
}
