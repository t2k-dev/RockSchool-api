using RockSchool.Domain.Enums;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Subscriptions.Payments
{
    public class PaymentService(IPaymentRepository paymentRepository, ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork) : IPaymentService
    {
        public async Task<Payment[]> GetBySubscriptionIdAsync(Guid subscriptionId)
        {
            return await paymentRepository.GetBySubscriptionIdAsync(subscriptionId);
        }

        public async Task Pay(Guid subscriptionId, decimal amount, int paymentType, DateTime paidOn)
        {
            var subscription = await subscriptionRepository.GetAsync(subscriptionId);
            if (subscription == null)
                throw new InvalidOperationException($"Subscription with id {subscriptionId} not found");

            var payment = Payment.Create(
                amount,
                paidOn.ToUniversalTime(),
                (PaymentType)paymentType,
                subscriptionId);

            subscription.RecordPayment(payment);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
