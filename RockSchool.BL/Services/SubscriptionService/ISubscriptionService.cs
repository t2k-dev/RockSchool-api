using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface ISubscriptionService
    {
        Task<SubscriptionDto?> GetAsync(Guid subscriptionId);
        Task<SubscriptionDto[]> GetSubscriptionsByStudentId(Guid studentId);
        Task<SubscriptionDto[]> GetSubscriptionsByTeacherId(Guid teacherId);
        Task<SubscriptionDto[]?> GetSubscriptionByGroupIdAsync(Guid groupId);
        Task<AvailableSlot> GetNextAvailableSlotAsync(Guid subscriptionId);
        Task AddSubscriptionAsync(SubscriptionDetails subscriptionDetails, Guid[] studentIds, ScheduleDto[] schedules);
        Task<Guid> AddTrialSubscriptionAsync(TrialRequestDto request);
        Task<AttendanceDto> RescheduleAttendance(Guid attendanceId, DateTime startDate);
        Task AcceptTrialSubscription(Guid subscriptionId, string statusReason);
        Task DeclineTrialSubscription(Guid subscriptionId, string statusReason);
    }
}
