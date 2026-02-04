using RockSchool.Domain.Enums;

namespace RockSchool.Domain.Entities;

public class Attendee
{
    public Guid SubscriptionAttendanceId { get; private set; }
    public Guid SubscriptionId { get; private set; }
    public Subscription Subscription { get; private set; }
    public Guid AttendanceId { get; private set; }
    public Attendance Attendance { get; private set; }
    public AttendanceStatus Status { get; private set; }
    //public string? StatusReason { get; private set; }

    private Attendee() { }

    public static Attendee Create(
        Guid subscriptionId,
        Guid attendanceId,
        AttendanceStatus status = AttendanceStatus.New)
    {
        if (subscriptionId == Guid.Empty)
            throw new ArgumentException("Subscription ID is required", nameof(subscriptionId));

        if (attendanceId == Guid.Empty)
            throw new ArgumentException("Attendance ID is required", nameof(attendanceId));

        return new Attendee
        {
            SubscriptionAttendanceId = Guid.NewGuid(),
            SubscriptionId = subscriptionId,
            AttendanceId = attendanceId,
            Status = status
        };
    }

    public void MarkAsAttended(string? reason = null)
    {
        Status = AttendanceStatus.Attended;
        //StatusReason = reason;
    }

    public void MarkAsMissed(string? reason = null)
    {
        Status = AttendanceStatus.Missed;
        //StatusReason = reason;
    }

    public void Cancel(string? reason = null)
    {
        Status = AttendanceStatus.CanceledByAdmin;
        //StatusReason = reason;
    }

    public void UpdateStatus(AttendanceStatus status, string? reason = null)
    {
        Status = status;
        //StatusReason = reason;
    }
}