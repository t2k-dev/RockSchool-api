using RockSchool.Domain.Enums;

namespace RockSchool.Domain.Entities;

public class Attendee
{
    public Guid AttendeeId { get; private set; }
    public Guid SubscriptionId { get; private set; }
    public Guid StudentId { get; private set; }
    public Guid AttendanceId { get; private set; }
    public AttendeeStatus Status { get; private set; }

    private Attendee() { }

    public static Attendee Create(
        Guid subscriptionId,
        Guid attendanceId,
        Guid studentId
        )
    {
        if (subscriptionId == Guid.Empty)
            throw new ArgumentException("Subscription ID is required", nameof(subscriptionId));

        if (attendanceId == Guid.Empty)
            throw new ArgumentException("Attendance ID is required", nameof(attendanceId));

        return new Attendee
        {
            AttendeeId = Guid.NewGuid(),
            SubscriptionId = subscriptionId,
            StudentId = studentId,
            AttendanceId = attendanceId,
            Status = AttendeeStatus.New,
        };
    }

    public void MarkAsAttended(string? reason = null)
    {
        Status = AttendeeStatus.Attended;
    }

    public void MarkAsMissed(string? reason = null)
    {
        Status = AttendeeStatus.Missed;
    }

    public void MarkAsCanceled(string? reason = null)
    {
        Status = AttendeeStatus.Canceled;
    }
}