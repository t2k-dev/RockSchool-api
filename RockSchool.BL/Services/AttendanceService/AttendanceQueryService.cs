using Microsoft.EntityFrameworkCore;
using RockSchool.BL.Models.Dtos;
using RockSchool.Data.Data;

namespace RockSchool.BL.Services.AttendanceService;

public class AttendanceQueryService(RockSchoolContext context) : IAttendanceQueryService
{
    public async Task<AttendanceWithAttendeesDto[]> GetByTeacherIdForPeriodAsync(Guid teacherId, DateTime startDate, DateTime endDate)
    {
        return await context.Attendances
            .Where(a => a.TeacherId == teacherId
                        && a.StartDate >= startDate
                        && a.EndDate <= endDate)
            .Select(a => new AttendanceWithAttendeesDto
            {
                AttendanceId = a.AttendanceId,
                TeacherId = a.TeacherId.Value,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                Status = (int)a.Status,
                Attendees = context.Attendees
                    .Where(att => att.AttendanceId == a.AttendanceId)
                    .Join(
                        context.Subscriptions,
                        att => att.SubscriptionId,
                        s => s.SubscriptionId,
                        (att, s) => new { Attendee = att, Subscription = s })
                    .Join(
                        context.Students,
                        x => x.Subscription.StudentId,
                        st => st.StudentId,
                        (x, st) => new AttendeeDto
                        {
                            AttendeeId = x.Attendee.AttendeeId,
                            SubscriptionId = x.Attendee.SubscriptionId,
                            StudentId = x.Attendee.StudentId,
                            Status = (int)x.Attendee.Status,
                            Student = new StudentInfoDto
                            {
                                StudentId = st.StudentId,
                                FirstName = st.FirstName,
                                LastName = st.LastName
                            },
                            Subscription = new SubscriptionInfoDto
                            {
                                SubscriptionId = x.Subscription.SubscriptionId,
                                GroupId = x.Subscription.GroupId,
                                AttendanceCount = x.Subscription.AttendanceCount,
                                AttendanceLength = x.Subscription.AttendanceLength,
                                AttendancesLeft = x.Subscription.AttendancesLeft,
                                StartDate = x.Subscription.StartDate,
                                Status = (int)x.Subscription.Status,
                                SubscriptionType = (int)x.Subscription.SubscriptionType,
                                DisciplineId = x.Subscription.DisciplineId
                            }
                        })
                    .ToList()
            })
            .ToArrayAsync();
    }
}
