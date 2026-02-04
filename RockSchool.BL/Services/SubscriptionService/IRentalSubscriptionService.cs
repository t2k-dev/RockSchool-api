
using RockSchool.BL.Models;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface IRentalSubscriptionService
    {
        Task<Guid> AddRentalSubscription(SubscriptionDetails details, Schedule[] schedules);
    }
}
