using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.Domain.Enums;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class PaymentService(TenderRepository tenderRepository, ISubscriptionService subscriptionService) : IPaymentService
    {
        public async Task Pay(Guid subscriptionId, Tender tender)
        {
            throw new NotImplementedException();
            /*
            // Add tender
            await tenderRepository.AddAsync(tender);

            // Update subscription
            var subscription = await subscriptionService.GetAsync(subscriptionId);
            var newAmountOutstanding = subscription.AmountOutstanding - tender.Amount;

            if (newAmountOutstanding == 0)
            {
                subscription.Status = SubscriptionStatus.Active;
            }

            subscription.AmountOutstanding = newAmountOutstanding;

            await subscriptionService.UpdateSubscriptionAsync(subscription);*/
        }
    }
}
