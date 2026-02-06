using RockSchool.Domain.Enums;
using RockSchool.Domain.Teachers;

namespace RockSchool.Domain.Entities;

public class Attendance
{
    public Guid AttendanceId { get; private set; }
    public int? DisciplineId { get; private set; }
    public Discipline? Discipline { get; private set; }
    public Guid? TeacherId { get; private set; }
    public Teacher? Teacher { get; private set; }
    public AttendanceStatus Status { get; private set; }
    public string? StatusReason { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public Guid? GroupId { get; private set; }
    public int BranchId { get; private set; }
    public Branch Branch { get; private set; }
    public int RoomId { get; private set; }
    public Room Room { get; private set; }
    public string? Comment { get; private set; }
    public bool IsCompleted { get; private set; }
    public AttendanceType AttendanceType { get; private set; }

    private readonly List<Attendee> _attendees = new();
    public IReadOnlyCollection<Attendee> Attendees => _attendees.AsReadOnly();

    private Attendance() { }

    public static Attendance Create(
        DateTime startDate,
        DateTime endDate,
        int roomId,
        int branchId,
        AttendanceType attendanceType,
        int? disciplineId = null,
        Guid? teacherId = null,
        Guid? groupId = null)
    {
        return new Attendance
        {
            AttendanceId = Guid.NewGuid(),
            StartDate = startDate,
            EndDate = endDate,
            RoomId = roomId,
            BranchId = branchId,
            AttendanceType = attendanceType,
            DisciplineId = disciplineId,
            TeacherId = teacherId,
            GroupId = groupId,
            Status = AttendanceStatus.New,
            IsCompleted = false
        };
    }

    public void MarkAsAttended(string? reason = null)
    {
        Status = AttendanceStatus.Attended;
        StatusReason = reason;
        IsCompleted = true;
    }

    public void MarkAsMissed(string? reason = null)
    {
        Status = AttendanceStatus.Missed;
        StatusReason = reason;
        IsCompleted = true;
    }

    public void Cancel(string? reason = null)
    {
        Status = AttendanceStatus.CanceledByAdmin;
        StatusReason = reason;
    }

    public void AddComment(string comment)
    {
        Comment = comment;
    }

    public void UpdateSchedule(DateTime startDate, DateTime endDate, int roomId)
    {
        StartDate = startDate;
        EndDate = endDate;
        RoomId = roomId;
    }
}
