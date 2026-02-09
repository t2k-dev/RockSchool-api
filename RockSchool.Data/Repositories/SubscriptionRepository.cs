using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.Data.Repositories;

public class SubscriptionRepository : BaseRepository, ISubscriptionRepository
{
    public SubscriptionRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
    {
    }

    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        await RockSchoolContext.Subscriptions.AddAsync(subscription);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task<Subscription?> GetAsync(Guid id)
    {
        return await RockSchoolContext.Subscriptions.FindAsync(id);
    }

    public async Task<Subscription[]> GetAllSubscriptionsAsync()
    {
        return await RockSchoolContext.Subscriptions.ToArrayAsync();
    }

    public async Task<Subscription[]?> GetSubscriptionsByStudentIdAsync(Guid studentId)
    {
        return await RockSchoolContext.Subscriptions
            .Where(a => a.StudentId == studentId)
            .Include(s => s.Teacher)
            .Include(s => s.Schedules)
            .ToArrayAsync();
    }

    public async Task<Subscription[]?> GetSubscriptionsByTeacherIdAsync(Guid teacherId)
    {
        return await RockSchoolContext.Subscriptions
            .Where(s => s.TeacherId == teacherId)
            .Include(s => s.Student)
            .ToArrayAsync();
    }

    public async Task<Subscription[]?> GetByGroupIdAsync(Guid groupId)
    {
        return await RockSchoolContext.Subscriptions
            .Where(a => a.GroupId == groupId)
            .ToArrayAsync();
    }

    public async Task UpdateSubscriptionAsync(Subscription subscription)
    {
        RockSchoolContext.Subscriptions.Update(subscription);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteSubscriptionAsync(Subscription subscription)
    {
        RockSchoolContext.Subscriptions.Remove(subscription);
        await RockSchoolContext.SaveChangesAsync();
    }
}