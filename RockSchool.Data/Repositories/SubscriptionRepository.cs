using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.Data.Repositories;

public class SubscriptionRepository(RockSchoolContext rockSchoolContext)
    : BaseRepository(rockSchoolContext), ISubscriptionRepository
{
    public async Task AddAsync(Subscription subscription)
    {
        await RockSchoolContext.Subscriptions.AddAsync(subscription);
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

    public void Update(Subscription subscription)
    {
        RockSchoolContext.Subscriptions.Update(subscription);
    }

    public async Task DeleteSubscriptionAsync(Subscription subscription)
    {
        RockSchoolContext.Subscriptions.Remove(subscription);
        await RockSchoolContext.SaveChangesAsync();
    }
}