using RockSchool.BL.Models;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface ITenderService
    {
        Task<Tender[]> GetTendersBySubscriptionIdAsync(Guid subscriptionId);
    }
}