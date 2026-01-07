using RockSchool.BL.Models;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface IRentalSubscriptionService
    {
        Task<Guid> AddRentalSubscription(SubscriptionDetails details, Schedule[] schedules);
    }
}
