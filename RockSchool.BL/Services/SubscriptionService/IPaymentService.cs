using RockSchool.BL.Models;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface IPaymentService
    {
        Task Pay(Guid subscriptionId, Payment payment);
    }
}
