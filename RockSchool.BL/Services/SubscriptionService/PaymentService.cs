using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.Data.Enums;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class PaymentService(TenderRepository tenderRepository, ISubscriptionService subscriptionService) : IPaymentService
    {
        public async Task Pay(Guid subscriptionId, Tender tender)
        {
            // Add tender
            var tenderEntity = tender.ToEntity();
            await tenderRepository.AddAsync(tenderEntity);

            // Update subscription
            var subscription = await subscriptionService.GetAsync(subscriptionId);
            var newAmountOutstanding = subscription.AmountOutstanding - tender.Amount;

            if (newAmountOutstanding == 0)
            {
                subscription.Status = SubscriptionStatus.Active;
            }

            subscription.AmountOutstanding = newAmountOutstanding;

            await subscriptionService.UpdateSubscriptionAsync(subscription);
        }
    }
}
