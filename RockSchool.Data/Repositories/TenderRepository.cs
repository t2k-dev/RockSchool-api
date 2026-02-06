using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Tenders;

namespace RockSchool.Data.Repositories
{
    public class TenderRepository(RockSchoolContext rockSchoolContext) : BaseRepository(rockSchoolContext), ITenderRepository
    {
        public async Task<Guid> AddAsync(Tender tender)
        {
            await RockSchoolContext.Tenders.AddAsync(tender);
            await RockSchoolContext.SaveChangesAsync();
            return tender.TenderId;
        }

        public async Task<Tender[]> GetBySubscriptionIdAsync(Guid subscriptionId)
        {
            return await RockSchoolContext.Tenders
                .Where(t => t.SubscriptionId == subscriptionId)
                .ToArrayAsync();
        }
    }
}
