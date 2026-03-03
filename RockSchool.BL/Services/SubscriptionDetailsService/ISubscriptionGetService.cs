using RockSchool.BL.Subscriptions;

namespace RockSchool.BL.Services.SubscriptionDetailsService;

public interface ISubscriptionGetService
{
    Task<SubscriptionDetailsResult> Query(Guid subscriptionId);
}
