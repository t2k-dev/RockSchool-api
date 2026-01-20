using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class TenderService(TenderRepository tenderRepository) : ITenderService
    {
        public async Task<Tender[]> GetTendersBySubscriptionIdAsync(Guid subscriptionId)
        {
            var tenderEntities = await tenderRepository.GetBySubscriptionIdAsync(subscriptionId);
            return tenderEntities.ToModel();
        }
    }
}