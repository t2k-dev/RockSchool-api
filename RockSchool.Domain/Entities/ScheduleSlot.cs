namespace RockSchool.Domain.Entities;

public class ScheduleSlot
{
    public Guid ScheduleSlotId { get; private set; }
    public Guid ScheduleId { get; private set; }
    public int RoomId { get; private set; }
    public Room? Room { get; private set; }
    public int WeekDay { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }

    // Constructor for EF
    private ScheduleSlot() { }

    // Factory method for creation
    public static ScheduleSlot Create(
        Guid scheduleId,
        int roomId,
        int weekDay,
        TimeSpan startTime,
        TimeSpan endTime)
    {
        if (startTime >= endTime)
            throw new InvalidOperationException("Start time must be before end time");

        if (weekDay < 0 || weekDay > 6)
            throw new InvalidOperationException("Week day must be between 0 and 6");

        return new ScheduleSlot
        {
            ScheduleSlotId = Guid.NewGuid(),
            ScheduleId = scheduleId,
            RoomId = roomId,
            WeekDay = weekDay,
            StartTime = startTime,
            EndTime = endTime
        };
    }

    public void UpdateTime(TimeSpan startTime, TimeSpan endTime)
    {
        if (startTime >= endTime)
            throw new InvalidOperationException("Start time must be before end time");

        StartTime = startTime;
        EndTime = endTime;
    }

    public void UpdateRoom(int roomId)
    {
        RoomId = roomId;
    }
}
