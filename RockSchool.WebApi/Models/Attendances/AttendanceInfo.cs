using System;
using RockSchool.WebApi.Models.Subscriptions;

namespace RockSchool.WebApi.Models.Attendances
{
    public class AttendanceInfo
    {
        public Guid AttendanceId { get; set; }
        public Guid SubscriptionId { get; set; }
        public SubscriptionInfo Subscription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public string StatusReason { get; set; }
        public int RoomId { get; set; }
        public int? DisciplineId { get; set; }
        public Guid StudentId { get; set; }
        public object Student { get; set; }
        public object Teacher { get; set; }
        public bool IsCompleted { get; set; }
        public Guid? GroupId { get; set; }
        public string Comment { get; set; }
        public int AttendanceType { get; set; }
    }
}
