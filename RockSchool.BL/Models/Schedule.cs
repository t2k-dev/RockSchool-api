namespace RockSchool.BL.Models;

public class Schedule
{
    public Guid ScheduleId { get; set; }
    public Guid? SubscriptionId { get; set; }
    public Subscription? Subscription { get; set; }
    public Guid? BandId { get; set; }
    public Band? Band { get; set; }
    public int WeekDay { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int RoomId { get; set; }
}