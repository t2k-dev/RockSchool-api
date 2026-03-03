using System;

namespace RockSchool.WebApi.Models
{
    public class ScheduleSlotInfo
    {
        public Guid ScheduleId { get; set; }
        public int WeekDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int RoomId { get; set; }
    }
}
