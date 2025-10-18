using System.Collections.Generic;
using RockSchool.BL.Dtos;
using RockSchool.WebApi.Models.Subscriptions;

namespace RockSchool.WebApi.Models.Teachers
{
    public class TeacherScreenDetailsInfo
    {
        public TeacherInfo Teacher { get; set; }
        public SubscriptionInfo[] Subscriptions { get; set; }
        public AttendanceInfo[] Attendances { get; set; }
    }
}
