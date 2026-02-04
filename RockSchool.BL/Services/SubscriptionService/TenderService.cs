using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class TenderService(TenderRepository tenderRepository) : ITenderService
    {
        public async Task<Tender[]> GetTendersBySubscriptionIdAsync(Guid subscriptionId)
        {
            return await tenderRepository.GetBySubscriptionIdAsync(subscriptionId);
        }
    }
}