using RockSchool.BL.Models;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Subscriptions
{
    public interface IRentalSubscriptionService
    {
        Task<Guid> AddRentalSubscription(SubscriptionDetails details, ScheduleSlotDto[] scheduleDtos);
    }
}
