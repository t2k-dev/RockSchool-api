using System;
using RockSchool.WebApi.Models.Teachers;

namespace RockSchool.WebApi.Models
{
    public class SubscriptionInfo
    {
        public Guid SubscriptionId { get; set; }

        public AttendanceInfo[] Attendances { get; set; }

        public bool IsTrial { get; set; }

        public int DisciplineId { get; set; }

        //TODO: Fix type
        public object Teacher { get; set; }

        public object Student { get; set; }

        public int Status { get; set; }

        public int? AttendanceCount { get; set; }
    }
}
