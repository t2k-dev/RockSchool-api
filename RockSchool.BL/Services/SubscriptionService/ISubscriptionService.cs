using RockSchool.BL.Models;
using RockSchool.Domain.Entities;

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
        Task<Guid> AddSubscriptionAsync(Subscription subscription);
        Task DecreaseAttendancesLeftCount(Guid subscriptionId);
        Task UpdateSubscriptionAsync(Subscription subscription);
        Task LinkPaymentToSubscription(Guid subscriptionId, Guid paymentId);
    }
}
