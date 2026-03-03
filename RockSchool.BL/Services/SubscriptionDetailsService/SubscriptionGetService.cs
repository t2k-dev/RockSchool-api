using RockSchool.BL.Services.SubscriptionService;
using RockSchool.BL.Subscriptions;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Services.SubscriptionDetailsService;

public class SubscriptionGetService(
    ISubscriptionService subscriptionService,
    IScheduleSlotRepository scheduleSlotRepository,
    IPaymentRepository paymentRepository
    ) : ISubscriptionGetService
{
    public async Task<SubscriptionDetailsResult> Query(Guid subscriptionId)
    {
        var subscription = await subscriptionService.GetAsync(subscriptionId);
        if (subscription == null)
            throw new InvalidOperationException($"Subscription with id {subscriptionId} not found");

        // Get schedule slots for this subscription's schedule
        var scheduleSlots = subscription.ScheduleId != null
            ? await scheduleSlotRepository.GetByScheduleIdAsync(subscription.ScheduleId.Value)
            : [];

        // Get payments
        var payments = await paymentRepository.GetBySubscriptionIdAsync(subscriptionId);

        return new SubscriptionDetailsResult
        {
            Subscription = subscription,
            ScheduleSlots = scheduleSlots ?? [],
            Payments = payments ?? []
        };
    }
}
