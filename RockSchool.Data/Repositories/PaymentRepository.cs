using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Repositories
{
    public class PaymentRepository(RockSchoolContext rockSchoolContext) : BaseRepository(rockSchoolContext)
    {
        public async Task<Guid> AddAsync(PaymentEntity paymentEntity)
        {
            await RockSchoolContext.Payments.AddAsync(paymentEntity);
            await RockSchoolContext.SaveChangesAsync();
            return paymentEntity.PaymentId;
        }
    }
}
