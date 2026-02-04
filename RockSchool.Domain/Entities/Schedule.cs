namespace RockSchool.Domain.Entities;

public class Schedule
{
    public Guid ScheduleId { get; private set; }
    public Guid? SubscriptionId { get; private set; }
    public Subscription? Subscription { get; private set; }
    public Guid? BandId { get; private set; }
    public Band? Band { get; private set; }
    public int RoomId { get; private set; }
    public Room? Room { get; private set; }
    public int WeekDay { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }

    private Schedule() { }

    public static Schedule Create(
        int roomId,
        int weekDay,
        TimeSpan startTime,
        TimeSpan endTime,
        Guid? subscriptionId = null,
        Guid? bandId = null)
    {
        if (startTime >= endTime)
            throw new InvalidOperationException("Start time must be before end time");

        if (weekDay < 0 || weekDay > 6)
            throw new InvalidOperationException("Week day must be between 0 and 6");

        return new Schedule
        {
            ScheduleId = Guid.NewGuid(),
            RoomId = roomId,
            WeekDay = weekDay,
            StartTime = startTime,
            EndTime = endTime,
            SubscriptionId = subscriptionId,
            BandId = bandId
        };
    }

    public void UpdateTime(TimeSpan startTime, TimeSpan endTime)
    {
        if (startTime >= endTime)
            throw new InvalidOperationException("Start time must be before end time");

        StartTime = startTime;
        EndTime = endTime;
    }

    public void ChangeRoom(int roomId)
    {
        RoomId = roomId;
    }
}
