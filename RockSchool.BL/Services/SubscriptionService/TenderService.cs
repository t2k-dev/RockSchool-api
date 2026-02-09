using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class TenderService(ITenderRepository tenderRepository) : ITenderService
    {
        public async Task<Tender[]> GetTendersBySubscriptionIdAsync(Guid subscriptionId)
        {
            return await tenderRepository.GetBySubscriptionIdAsync(subscriptionId);
        }
    }
}