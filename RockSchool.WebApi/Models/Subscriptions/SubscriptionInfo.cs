using RockSchool.BL.Dtos;
using RockSchool.Data.Enums;
using System;

namespace RockSchool.WebApi.Models.Subscriptions
{
    public class SubscriptionInfo
    {
        public Guid SubscriptionId { get; set; }

        public AttendanceInfo[] Attendances { get; set; }

        public DateTime StartDate { get; set; }

        public bool IsTrial { get; set; }

        public int DisciplineId { get; set; }

        //TODO: Fix type
        public Guid TeacherId { get; set; }
        public object Teacher { get; set; }

        public Guid StudentId { get; set; }

        public object Student { get; set; }

        public int Status { get; set; }

        public int? AttendanceCount { get; set; }

        public int? AttendanceLength { get; set; }

        public TrialStatus? TrialStatus { get; set; }

        public ScheduleInfo[] Schedules { get; set; }
    }
}
