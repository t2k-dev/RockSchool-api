using RockSchool.BL.Models;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.Data.Enums;
using RockSchool.Data.Repositories;
using RockSchool.BL.Helpers;

namespace RockSchool.BL.Services.AttendanceService
{
    public class AttendanceSubmitService : IAttendanceSubmitService
    {
        private readonly AttendanceRepository _attendanceRepository;
        private readonly ITrialSubscriptionService _trialSubscriptionService;
        private readonly ISubscriptionService _subscriptionService;

        public AttendanceSubmitService(AttendanceRepository attendanceRepository, ITrialSubscriptionService trialSubscriptionService, ISubscriptionService subscriptionService)
        {
            _attendanceRepository = attendanceRepository;
            _trialSubscriptionService = trialSubscriptionService;
            _subscriptionService = subscriptionService;
        }

        public async Task SubmitAttendance(Guid attendanceId, int status, string statusReason, string comment)
        {
            var submittedAttendance = await SubmitSingleAttendance(attendanceId, status, statusReason, comment);

            await _subscriptionService.DecreaseAttendancesLeftCount(submittedAttendance.SubscriptionId);
        }

        public async Task AcceptTrial(Guid attendanceId, string statusReason, string comment)
        {
            // Update attendance
            var submittedAttendance = await SubmitSingleAttendance(attendanceId, (int)AttendanceStatus.Attended, statusReason, comment);

            // Update subscription
            await _trialSubscriptionService.CompleteTrial(submittedAttendance.SubscriptionId, TrialStatus.Positive, statusReason);
        }

        public async Task DeclineTrial(Guid attendanceId, string statusReason, string comment)
        {
            // Update attendance
            var submittedAttendance = await SubmitSingleAttendance(attendanceId, (int)AttendanceStatus.Attended, statusReason, comment);

            // Update subscription
            await _trialSubscriptionService.CompleteTrial(submittedAttendance.SubscriptionId, TrialStatus.Negative, statusReason);
        }

        public async Task MissedTrial(Guid attendanceId, string statusReason, string comment)
        {
            throw new NotImplementedException("MissedTrial");
        }

        private async Task<Attendance> SubmitSingleAttendance(Guid attendanceId, int status, string statusReason, string comment)
        {
            var attendanceEntity = await _attendanceRepository.GetAsync(attendanceId);

            attendanceEntity.Status = (AttendanceStatus)status;
            attendanceEntity.StatusReason = statusReason;
            attendanceEntity.IsCompleted = true;
            attendanceEntity.Comment = comment;

            await _attendanceRepository.UpdateAsync(attendanceEntity);

            return attendanceEntity.ToModel();
        }

    }
}
