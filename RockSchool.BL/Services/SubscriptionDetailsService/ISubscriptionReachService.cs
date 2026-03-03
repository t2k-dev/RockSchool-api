using RockSchool.BL.Subscriptions;

namespace RockSchool.BL.Services.SubscriptionDetailsService;

public interface ISubscriptionReachService
{
    Task<SubscriptionDetailsResult> Query(Guid subscriptionId);
}
