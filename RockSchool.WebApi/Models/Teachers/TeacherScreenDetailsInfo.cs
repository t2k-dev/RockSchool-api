using System.Collections.Generic;
using RockSchool.BL.Dtos;

namespace RockSchool.WebApi.Models.Teachers
{
    public class TeacherScreenDetailsInfo
    {
        public TeacherInfo Teacher { get; set; }
        public SubscriptionInfo[] Subscriptions { get; set; }
        public AttendanceInfo[] Attendances { get; set; }
    }
}
