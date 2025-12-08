using RockSchool.BL.Models;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface ISubscriptionService
    {
        Task<Subscription?> GetAsync(Guid subscriptionId);
        Task<Subscription[]> GetSubscriptionsByStudentId(Guid studentId);
        Task<Subscription[]?> GetSubscriptionsByTeacherId(Guid teacherId);
        Task<Subscription[]?> GetSubscriptionByGroupIdAsync(Guid groupId);
        Task<AvailableSlot> GetNextAvailableSlotAsync(Guid subscriptionId);
        Task AddSubscriptionAsync(SubscriptionDetails subscriptionDetails, Guid[] studentIds, ScheduleDto[] schedules);
        Task<Guid> AddTrialSubscriptionAsync(TrialRequestDto request);
        Task<Attendance> RescheduleAttendance(Guid attendanceId, DateTime startDate);
        Task AcceptTrialSubscription(Guid subscriptionId, string statusReason);
        Task DeclineTrialSubscription(Guid subscriptionId, string statusReason);
    }
}
