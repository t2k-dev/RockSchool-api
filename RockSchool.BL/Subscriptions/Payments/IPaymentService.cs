using RockSchool.BL.Models;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;

namespace RockSchool.BL.Subscriptions.Payments
{
    public interface IPaymentService
    {
        Task Pay(Guid subscriptionId, decimal amount, int paymentType, DateTime paidOn);
        Task<Payment[]> GetBySubscriptionIdAsync(Guid subscriptionId);
    }
}
