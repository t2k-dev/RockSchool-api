using RockSchool.Data.Enums;
using RockSchool.WebApi.Models.Attendances;
using System;

namespace RockSchool.WebApi.Models.Subscriptions
{
    public class SubscriptionInfo
    {
        public Guid SubscriptionId { get; set; }

        public AttendanceInfo[] Attendances { get; set; }

        public DateOnly StartDate { get; set; }

        public bool IsTrial { get; set; }

        public int? DisciplineId { get; set; }

        //TODO: Fix type
        public Guid? TeacherId { get; set; }
        public object? Teacher { get; set; }

        public Guid StudentId { get; set; }

        public object Student { get; set; }

        public int Status { get; set; }

        public int? AttendanceCount { get; set; }

        public int? AttendanceLength { get; set; }

        public TrialStatus? TrialStatus { get; set; }

        public int SubscriptionType { get; set; }

        public Guid? GroupId { get; set; }

        public ScheduleInfo[] Schedules { get; set; }

        /// <summary>
        /// Additional field.
        /// </summary>
        public int AttendancesLeft { get; set; }

        public decimal AmountOutstanding { get; set; }

        public decimal Price { get; set; }

        public decimal FinalPrice { get; set; }

        public TenderInfo[] Tenders { get; set; }

        public Guid? PaymentId { get; set; }
    }
}
