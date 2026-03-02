using RockSchool.BL.Models;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Subscriptions.Payments
{
    public interface IPaymentService
    {
        Task Pay(Guid subscriptionId, Payment payment);
        Task<Payment[]> GetBySubscriptionIdAsync(Guid subscriptionId);
    }
}
