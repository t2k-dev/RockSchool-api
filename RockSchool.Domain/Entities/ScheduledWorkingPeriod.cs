namespace RockSchool.Domain.Entities;

public class ScheduledWorkingPeriod
{
    public Guid ScheduledWorkingPeriodId { get; private set; }
    public Guid? WorkingPeriodId { get; private set; }
    public WorkingPeriod WorkingPeriod { get; private set; }
    public Guid TeacherId { get; private set; }
    public Teacher Teacher { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public int RoomId { get; private set; }
    public Room Room { get; private set; }

    private ScheduledWorkingPeriod() { }

    public static ScheduledWorkingPeriod Create(
        Guid teacherId,
        DateTime startDate,
        DateTime endDate,
        int roomId,
        Guid? workingPeriodId = null)
    {
        if (teacherId == Guid.Empty)
            throw new ArgumentException("Teacher ID is required", nameof(teacherId));

        if (startDate >= endDate)
            throw new ArgumentException("Start date must be before end date");

        if (roomId <= 0)
            throw new ArgumentException("Room ID is required", nameof(roomId));

        return new ScheduledWorkingPeriod
        {
            ScheduledWorkingPeriodId = Guid.NewGuid(),
            WorkingPeriodId = workingPeriodId,
            TeacherId = teacherId,
            StartDate = startDate,
            EndDate = endDate,
            RoomId = roomId
        };
    }

    public void UpdateSchedule(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate)
            throw new ArgumentException("Start date must be before end date");

        StartDate = startDate;
        EndDate = endDate;
    }

    public void ChangeRoom(int roomId)
    {
        if (roomId <= 0)
            throw new ArgumentException("Room ID is required", nameof(roomId));

        RoomId = roomId;
    }
}
