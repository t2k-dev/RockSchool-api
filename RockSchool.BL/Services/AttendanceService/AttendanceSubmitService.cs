using RockSchool.BL.Models;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.Domain.Enums;
using RockSchool.Data.Repositories;
using RockSchool.BL.Helpers;
using RockSchool.Domain.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.BL.Subscriptions.Trial;

namespace RockSchool.BL.Services.AttendanceService
{
    public class AttendanceSubmitService(
        IAttendanceRepository attendanceRepository,
        ISubscriptionRepository subscriptionRepository,
        IUnitOfWork unitOfWork)
        : IAttendanceSubmitService
    {
        public async Task SubmitAttendance(Guid attendanceId, int status, string statusReason, string comment)
        {
            await SubmitSingleAttendance(attendanceId, (AttendanceStatus)status, statusReason);

            //await subscriptionService.DecreaseAttendancesLeftCount(submittedAttendance.SubscriptionId);

            await unitOfWork.SaveChangesAsync();
        }

        private async Task<Attendance> SubmitSingleAttendance(Guid attendanceId, AttendanceStatus status, string statusReason)
        {
            var attendance = await attendanceRepository.GetAsync(attendanceId);
            if (attendance == null)
                throw new InvalidOperationException($"Attendance with id {attendanceId} not found");

            switch (status)
            {
                case AttendanceStatus.Attended:
                    attendance.MarkAsAttended(statusReason);
                    break;
                case AttendanceStatus.Missed:
                    attendance.MarkAsMissed(statusReason);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }

            return attendance;
        }
    }
}
