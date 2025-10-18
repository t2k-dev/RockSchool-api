﻿using System.Collections.Generic;
using RockSchool.BL.Dtos;
using RockSchool.WebApi.Models.Subscriptions;

namespace RockSchool.WebApi.Models.Students
{
    public class StudentScreenDetailsInfo
    {
        public StudentDto Student { get; set; }
        public SubscriptionInfo[] Subscriptions { get; set; }
        public AttendanceInfo[] Attendances { get; set; }
    }
}
