using RockSchool.BL.Models;
using RockSchool.Data.Repositories;
using RockSchool.BL.Helpers;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class PaymentService(PaymentRepository paymentRepository, ISubscriptionService subscriptionService) : IPaymentService
    {
        public async Task Pay(Guid subscriptionId, Payment payment)
        {
            var paymentEntity = payment.ToEntity();
            var paymentId = await paymentRepository.AddAsync(paymentEntity);

            await subscriptionService.LinkPaymentToSubscription(subscriptionId, paymentId);
        }
    }
}
