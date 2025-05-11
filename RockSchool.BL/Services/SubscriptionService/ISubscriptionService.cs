using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface ISubscriptionService
    {
        Task<SubscriptionDto> GetAsync(Guid subscriptionId);
        Task<SubscriptionDto[]> GetSubscriptionsByStudentId(Guid studentId);
        Task<SubscriptionDto[]> GetSubscriptionsByTeacherId(Guid teacherId);
        Task<AvailableSlot> GetNextAvailableSlotAsync(Guid subscriptionId);
        Task<Guid> AddSubscriptionAsync(SubscriptionDto subscriptionDto);
        Task<Guid> AddTrialSubscriptionAsync(TrialRequestDto request);
        Task<AttendanceDto> RescheduleAttendance(Guid attendanceId, DateTime startDate);
        Task DeclineTrialSubscription(Guid subscriptionId, string statusReason);
    }
}
