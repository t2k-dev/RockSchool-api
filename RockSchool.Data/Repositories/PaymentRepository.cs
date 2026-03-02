using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.Data.Repositories
{
    public class PaymentRepository(RockSchoolContext rockSchoolContext) : BaseRepository(rockSchoolContext), IPaymentRepository
    {
        public async Task<Guid> AddAsync(Payment payment)
        {
            await RockSchoolContext.Payments.AddAsync(payment);
            await RockSchoolContext.SaveChangesAsync();
            return payment.PaymentId;
        }

        public async Task<Payment[]> GetBySubscriptionIdAsync(Guid subscriptionId)
        {
            return await RockSchoolContext.Payments
                .Where(t => t.SubscriptionId == subscriptionId)
                .ToArrayAsync();
        }
    }
}
