using RockSchool.BL.Models;
using RockSchool.Data.Repositories;
using RockSchool.BL.Helpers;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class PaymentService(TenderRepository tenderRepository, ISubscriptionService subscriptionService) : IPaymentService
    {
        public async Task Pay(Guid subscriptionId, Tender tender)
        {
            var tenderEntity = tender.ToEntity();
            var tenderId = await tenderRepository.AddAsync(tenderEntity);

            //await subscriptionService.LinkPaymentToSubscription(subscriptionId, tenderId);
        }
    }
}
