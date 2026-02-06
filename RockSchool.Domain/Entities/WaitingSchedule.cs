using RockSchool.Domain.Students;
using RockSchool.Domain.Teachers;

namespace RockSchool.Domain.Entities;

public class WaitingSchedule
{
    public Guid ScheduleId { get; private set; }
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; }
    public int DisciplineId { get; private set; }
    public Discipline Discipline { get; private set; }
    public Guid TeacherId { get; private set; }
    public Teacher Teacher { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public int WeekDay { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    private WaitingSchedule() { }

    public static WaitingSchedule Create(
        Guid studentId,
        int disciplineId,
        Guid teacherId,
        int weekDay,
        DateTime startTime,
        DateTime endTime)
    {
        if (studentId == Guid.Empty)
            throw new ArgumentException("Student ID is required", nameof(studentId));

        if (disciplineId <= 0)
            throw new ArgumentException("Discipline ID is required", nameof(disciplineId));

        if (teacherId == Guid.Empty)
            throw new ArgumentException("Teacher ID is required", nameof(teacherId));

        if (weekDay < 0 || weekDay > 6)
            throw new ArgumentException("Week day must be between 0 and 6", nameof(weekDay));

        if (startTime >= endTime)
            throw new ArgumentException("Start time must be before end time");

        return new WaitingSchedule
        {
            ScheduleId = Guid.NewGuid(),
            StudentId = studentId,
            DisciplineId = disciplineId,
            TeacherId = teacherId,
            CreatedOn = DateTime.UtcNow,
            WeekDay = weekDay,
            StartTime = startTime,
            EndTime = endTime
        };
    }

    public void UpdateSchedule(int weekDay, DateTime startTime, DateTime endTime)
    {
        if (weekDay < 0 || weekDay > 6)
            throw new ArgumentException("Week day must be between 0 and 6", nameof(weekDay));

        if (startTime >= endTime)
            throw new ArgumentException("Start time must be before end time");

        WeekDay = weekDay;
        StartTime = startTime;
        EndTime = endTime;
    }

    public void ChangeTeacher(Guid teacherId)
    {
        if (teacherId == Guid.Empty)
            throw new ArgumentException("Teacher ID is required", nameof(teacherId));

        TeacherId = teacherId;
    }
}