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

        var attendee = attendance.Attendees.SingleOrDefault(a => a.AttendeeId == attendeeId);

        switch ((AttendeeStatus)attendeeStatus)
        {
            case AttendeeStatus.Attended:
                attendee.MarkAsAttended();
                await subscriptionService.ReduceAttendanceCount(attendee.SubscriptionId);

                break;
            case AttendeeStatus.Missed:
                attendee.MarkAsMissed();

                var subscription = await subscriptionRepository.GetAsync(attendee.SubscriptionId);

                if (attendance.AttendanceType == AttendanceType.TrialLesson)
                {
                    subscription.CompleteTrial(TrialDecision.Missed);
                }
                else
                {
                    subscription.ReduceAttendanceCount();
                }

                subscriptionRepository.Update(subscription);

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

        attendanceRepository.Update(attendance);


        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
