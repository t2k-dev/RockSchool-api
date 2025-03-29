using System;
using RockSchool.WebApi.Models.Teachers;

namespace RockSchool.WebApi.Models
{
    public class SubscriptionsInfo
    {
        public Guid SubscriptionId { get; set; }
        public AttendanceInfo[] Attendances { get; set; }
        public bool IsTrial { get; set; }
        public int DisciplineId { get; set; }
        public TeacherInfo Teacher { get; set; }
        public int Status { get; set; }
    }
}
