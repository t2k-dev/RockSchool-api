using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.Domain.Enums;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;
using RockSchool.BL.Services.SubscriptionService;

namespace RockSchool.BL.Subscriptions.Payments
{
    public class PaymentService(IPaymentRepository paymentRepository, ISubscriptionService subscriptionService) : IPaymentService
    {
        public async Task<Payment[]> GetBySubscriptionIdAsync(Guid subscriptionId)
        {
            return await paymentRepository.GetBySubscriptionIdAsync(subscriptionId);
        }

        public async Task Pay(Guid subscriptionId, Payment payment)
        {
            throw new NotImplementedException();
            /*
            // Add payment
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
