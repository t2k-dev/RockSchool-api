namespace RockSchool.Domain.Entities;

public class WorkingPeriod
{
    public Guid WorkingPeriodId { get; private set; }
    public Guid TeacherId { get; private set; }
    public Teacher Teacher { get; private set; }
    public int WeekDay { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }
    public int RoomId { get; private set; }
    public Room Room { get; private set; }

    private readonly List<ScheduledWorkingPeriod> _scheduledWorkingPeriods = new();
    public IReadOnlyCollection<ScheduledWorkingPeriod> ScheduledWorkingPeriods => _scheduledWorkingPeriods.AsReadOnly();

    private WorkingPeriod() { }

    public static WorkingPeriod Create(
        Guid teacherId,
        int weekDay,
        TimeSpan startTime,
        TimeSpan endTime,
        int roomId)
    {
        if (teacherId == Guid.Empty)
            throw new ArgumentException("Teacher ID is required", nameof(teacherId));

        if (weekDay < 0 || weekDay > 6)
            throw new ArgumentException("Week day must be between 0 and 6", nameof(weekDay));

        if (startTime >= endTime)
            throw new ArgumentException("Start time must be before end time");

        if (roomId <= 0)
            throw new ArgumentException("Room ID is required", nameof(roomId));

        return new WorkingPeriod
        {
            WorkingPeriodId = Guid.NewGuid(),
            TeacherId = teacherId,
            WeekDay = weekDay,
            StartTime = startTime,
            EndTime = endTime,
            RoomId = roomId
        };
    }

    public void UpdateTime(TimeSpan startTime, TimeSpan endTime)
    {
        if (startTime >= endTime)
            throw new ArgumentException("Start time must be before end time");

        StartTime = startTime;
        EndTime = endTime;
    }

    public void ChangeRoom(int roomId)
    {
        if (roomId <= 0)
            throw new ArgumentException("Room ID is required", nameof(roomId));

        RoomId = roomId;
    }

    public void ChangeWeekDay(int weekDay)
    {
        if (weekDay < 0 || weekDay > 6)
            throw new ArgumentException("Week day must be between 0 and 6", nameof(weekDay));

        WeekDay = weekDay;
    }
}
