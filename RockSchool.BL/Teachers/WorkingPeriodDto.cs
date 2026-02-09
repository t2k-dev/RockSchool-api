namespace RockSchool.BL.Teachers
{
    public class WorkingPeriodDto
    {
        public Guid WorkingPeriodId { get; set; }
        public Guid TeacherId { get; set; }
        public int WeekDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int RoomId { get; set; }
    }
}
