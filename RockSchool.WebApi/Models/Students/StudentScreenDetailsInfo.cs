using System.Collections.Generic;
using RockSchool.BL.Models;
using RockSchool.WebApi.Models.Attendances;
using RockSchool.WebApi.Models.Subscriptions;
using RockSchool.WebApi.Models.Bands;

namespace RockSchool.WebApi.Models.Students
{
    public class StudentScreenDetailsInfo
    {
        public Student Student { get; set; }
        public SubscriptionInfo[] Subscriptions { get; set; }
        public AttendanceInfo[] Attendances { get; set; }
        public BandInfo[] Bands { get; set; }
    }
}
