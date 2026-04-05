using RockSchool.BL.Subscriptions;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Services.AttendeeService;

public class AttendeeService(
    IAttendanceRepository attendanceRepository,
    ISubscriptionRepository subscriptionRepository,
    ISubscriptionService subscriptionService,
    IUnitOfWork unitOfWork) : IAttendeeService
{
    public async Task<bool> UpdateStatus(Guid attendanceId, Guid attendeeId, int attendeeStatus)
    {
        var attendance = await attendanceRepository.GetAsync(attendanceId);
        if (attendance == null)
            return false;

        var attendee = attendance.Attendees.SingleOrDefault(a => a.AttendeeId == attendeeId);
        if (attendee == null)
            return false;

        switch ((AttendeeStatus)attendeeStatus)
        {
            case AttendeeStatus.Attended:
                attendee.MarkAsAttended();
                await subscriptionService.ReduceAttendanceCount(attendee.SubscriptionId);

                break;
            case AttendeeStatus.Missed:
                attendee.MarkAsMissed();

                var subscription = await subscriptionRepository.GetAsync(attendee.SubscriptionId);
                if (subscription == null)
                    return false;

                if (attendance.AttendanceType == AttendanceType.TrialLesson)
                {
                    subscription.CompleteTrial(TrialDecision.Missed);
                }
                else
                {
                    subscription.ReduceAttendanceCount();
                }

                break;
        }

        if (attendance.Attendees.All(a => a.Status == AttendeeStatus.Missed))
        {
            attendance.MarkAsMissed();
        }
        else if (attendance.Attendees.All(a => a.Status != AttendeeStatus.New))
        {
            attendance.MarkAsAttended();
        }

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
