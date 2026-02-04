using RockSchool.BL.Models;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.SubscriptionService
{
    public interface IPaymentService
    {
        Task Pay(Guid subscriptionId, Tender payment);
    }
}
