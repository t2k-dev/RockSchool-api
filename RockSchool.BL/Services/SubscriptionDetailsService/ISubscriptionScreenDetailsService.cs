using RockSchool.BL.Models;

namespace RockSchool.BL.Services.SubscriptionDetailsService;

public interface ISubscriptionScreenDetailsService
{
    Task<SubscriptionScreenDetailsResult> Query(Guid subscriptionId);
}
