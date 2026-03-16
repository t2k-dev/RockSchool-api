using System;

namespace RockSchool.WebApi.Models.Subscriptions
{
    public class RescheduleAttendanceRequest
    {
        public Guid AttendanceId { get; set; }
        public DateTime NewStartDate { get; set; }
        public DateTime NewEndDate { get; set; }
        public int RoomId { get; set; }
        public string StatusReason { get; set; }
    }
}
