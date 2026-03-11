using RockSchool.BL.Models.Dtos;
using RockSchool.Domain.Enums;
using RockSchool.WebApi.Models.Bands;
using RockSchool.WebApi.Models.Students;
using System;

namespace RockSchool.WebApi.Models.Teachers
{
    public class TeacherScreenDetailsResponse
    {
        public TeacherInfo Teacher { get; set; }
        public TeacherSubscriptionInfo[] Subscriptions { get; set; }
        public AttendanceWithAttendeesDto[] Attendances { get; set; }
        public BandInfo[] Bands { get; set; }
    }

    public class TeacherSubscriptionInfo
    {
        public Guid SubscriptionId { get; set; }

        public DateOnly StartDate { get; set; }

        public int? DisciplineId { get; set; }

        public string StudentFullName { get; set; }

        public Guid StudentId { get; set; }

        public int Status { get; set; }

        public int? AttendanceCount { get; set; }

        public int? AttendanceLength { get; set; }

        public TrialDecision? TrialDecision { get; set; }

        public int SubscriptionType { get; set; }

        public Guid? GroupId { get; set; }

        /// <summary>
        /// Additional field.
        /// </summary>
        public int AttendancesLeft { get; set; }

        public decimal AmountOutstanding { get; set; }

        public decimal Price { get; set; }

        public decimal FinalPrice { get; set; }
    }
}
