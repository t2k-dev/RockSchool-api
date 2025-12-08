using System;

namespace RockSchool.WebApi.Models
{
    public class ScheduleInfo
    {
        public Guid ScheduleId { get; set; }
        public Guid SubscriptionId { get; set; }
        public int WeekDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int RoomId { get; set; }
    }
}
