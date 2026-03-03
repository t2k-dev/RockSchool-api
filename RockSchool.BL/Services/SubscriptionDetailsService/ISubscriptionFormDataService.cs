using RockSchool.BL.Subscriptions;

namespace RockSchool.BL.Services.SubscriptionDetailsService;

public interface ISubscriptionFormDataService
{
    Task<SubscriptionFormDataResult> Query(Guid subscriptionId);
}
