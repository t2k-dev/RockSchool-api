using RockSchool.BL.Models;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.Domain.Enums;
using RockSchool.Data.Repositories;
using RockSchool.BL.Helpers;
using RockSchool.Domain.Repositories;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.AttendanceService
{
    public class AttendanceSubmitService(
        IAttendanceRepository attendanceRepository,
        ITrialSubscriptionService trialSubscriptionService,
        ISubscriptionService subscriptionService)
        : IAttendanceSubmitService
    {
        public async Task SubmitAttendance(Guid attendanceId, int status, string statusReason, string comment)
        {
            var submittedAttendance = await SubmitSingleAttendance(attendanceId, status, statusReason, comment);

            //await subscriptionService.DecreaseAttendancesLeftCount(submittedAttendance.SubscriptionId);
        }

        public async Task AcceptTrial(Guid attendanceId, string statusReason, string comment)
        {
            // Update attendance
            var submittedAttendance = await SubmitSingleAttendance(attendanceId, (int)AttendanceStatus.Attended, statusReason, comment);

            // Update subscription
            //await trialSubscriptionService.CompleteTrial(submittedAttendance.SubscriptionId, TrialStatus.Positive, statusReason);
        }

        public async Task DeclineTrial(Guid attendanceId, string statusReason, string comment)
        {
            // Update attendance
            var submittedAttendance = await SubmitSingleAttendance(attendanceId, (int)AttendanceStatus.Attended, statusReason, comment);

            // Update subscription
            //await trialSubscriptionService.CompleteTrial(submittedAttendance.SubscriptionId, TrialStatus.Negative, statusReason);
        }

        public async Task MissedTrial(Guid attendanceId, string statusReason, string comment)
        {
            var submittedAttendance = await SubmitSingleAttendance(attendanceId, (int)AttendanceStatus.Missed, statusReason, comment);
            
            //await trialSubscriptionService.CompleteTrial(submittedAttendance.SubscriptionId, TrialStatus.Missed, statusReason);
        }

        private async Task<Attendance> SubmitSingleAttendance(Guid attendanceId, int status, string statusReason, string comment)
        {
            var attendanceEntity = await attendanceRepository.GetAsync(attendanceId);

            throw new NotImplementedException();
            /*
            attendanceEntity.Status = (AttendanceStatus)status;
            attendanceEntity.StatusReason = statusReason;
            attendanceEntity.IsCompleted = true;
            attendanceEntity.Comment = comment;

            await attendanceRepository.UpdateAsync(attendanceEntity);

            return attendanceEntity;*/
        }

    }
}
