using RockSchool.BL.Models;
using RockSchool.Data.Enums;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface ISubscriptionService
    {
        Task<Subscription?> GetAsync(Guid subscriptionId);
        Task<Subscription[]> GetSubscriptionsByStudentId(Guid studentId);
        Task<Subscription[]?> GetSubscriptionsByTeacherId(Guid teacherId);
        Task<Subscription[]?> GetSubscriptionByGroupIdAsync(Guid groupId);
        Task<AvailableSlot> GetNextAvailableSlotAsync(Guid subscriptionId);
        Task AddSubscriptionAsync(SubscriptionDetails subscriptionDetails, Guid[] studentIds, Schedule[] schedules);
        Task<Guid> AddSubscriptionAsync(Subscription subscription);
        Task DecreaseAttendancesLeftCount(Guid subscriptionId);
        Task LinkPaymentToSubscription(Guid subscriptionId, Guid paymentId);
    }
}
