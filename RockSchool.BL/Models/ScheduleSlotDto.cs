namespace RockSchool.BL.Models
{
    public class ScheduleSlotDto
    {
        public Guid ScheduleId { get; set; }
        public int WeekDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int RoomId { get; set; }
    }
}
