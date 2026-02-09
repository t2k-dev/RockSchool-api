using System;

namespace RockSchool.WebApi.Models.Teachers
{
    public class UpdateTeacherPeriodsRequest
    {
        public WorkingPeriodInfo[] WorkingPeriods { get; set; }
        public DateTime? PeriodsChangedFrom { get; set; }

    }
}
