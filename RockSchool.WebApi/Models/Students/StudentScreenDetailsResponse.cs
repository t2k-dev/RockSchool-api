using RockSchool.BL.Models.Dtos;
using RockSchool.Domain.Enums;
using RockSchool.WebApi.Models.Bands;
using System;

namespace RockSchool.WebApi.Models.Students
{
    public class StudentScreenDetailsResponse
    {
        public StudentInfo Student { get; set; }
        public SubscriptionInfo[] Subscriptions { get; set; }
        public AttendanceWithAttendeesDto[] Attendances { get; set; }
        public BandInfo[] Bands { get; set; }
    }

    public class SubscriptionInfo
    {
        public Guid SubscriptionId { get; set; }

        public DateOnly StartDate { get; set; }

        public int? DisciplineId { get; set; }

        public Guid? TeacherId { get; set; }
        
        public string TeacherFullName { get; set; }

        public Guid StudentId { get; set; }

        public int Status { get; set; }

        public int? AttendanceCount { get; set; }

        public int? AttendanceLength { get; set; }

        public TrialStatus? TrialStatus { get; set; }

        public int SubscriptionType { get; set; }

        public Guid? GroupId { get; set; }

        //public ScheduleInfo[] Schedules { get; set; }

        /// <summary>
        /// Additional field.
        /// </summary>
        public int AttendancesLeft { get; set; }

        public decimal AmountOutstanding { get; set; }

        public decimal Price { get; set; }

        public decimal FinalPrice { get; set; }

        //public TenderInfo[] Tenders { get; set; }
    }
}
