using RockSchool.BL.Models;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface ITenderService
    {
        Task<Tender[]> GetTendersBySubscriptionIdAsync(Guid subscriptionId);
    }
}