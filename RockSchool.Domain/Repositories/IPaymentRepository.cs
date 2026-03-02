using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IPaymentRepository
{
    Task<Guid> AddAsync(Payment payment);
    Task<Payment[]> GetBySubscriptionIdAsync(Guid subscriptionId);
}
