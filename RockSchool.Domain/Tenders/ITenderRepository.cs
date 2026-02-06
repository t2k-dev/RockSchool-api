using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Tenders
{
    public interface ITenderRepository
    {
        Task<Guid> AddAsync(Tender tender);
        Task<Tender[]> GetBySubscriptionIdAsync(Guid subscriptionId);
    }
}
