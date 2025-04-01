using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Repositories;

public class SubscriptionRepository : BaseRepository
{
    public SubscriptionRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
    {
    }

    public async Task AddSubscriptionAsync(SubscriptionEntity subscriptionEntity)
    {
        await RockSchoolContext.Subscriptions.AddAsync(subscriptionEntity);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task<SubscriptionEntity?> GetSubscriptionByIdAsync(Guid id)
    {
        return await RockSchoolContext.Subscriptions.FindAsync(id);
    }

    public async Task<SubscriptionEntity[]> GetAllSubscriptionsAsync()
    {
        return await RockSchoolContext.Subscriptions.ToArrayAsync();
    }

    public async Task UpdateSubscriptionAsync(SubscriptionEntity subscriptionEntity)
    {
        RockSchoolContext.Subscriptions.Update(subscriptionEntity);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteSubscriptionAsync(SubscriptionEntity subscriptionEntity)
    {
        RockSchoolContext.Subscriptions.Remove(subscriptionEntity);
        await RockSchoolContext.SaveChangesAsync();
    }
}