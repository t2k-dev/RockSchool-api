﻿using Microsoft.EntityFrameworkCore;
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

    public async Task<SubscriptionEntity?> GetAsync(Guid id)
    {
        return await RockSchoolContext.Subscriptions.FindAsync(id);
    }

    public async Task<SubscriptionEntity[]> GetAllSubscriptionsAsync()
    {
        return await RockSchoolContext.Subscriptions.ToArrayAsync();
    }

    public async Task<SubscriptionEntity[]?> GetSubscriptionsByStudentIdAsync(Guid studentId)
    {
        return await RockSchoolContext.Subscriptions
            .Where(a => a.StudentId == studentId)
            .Include(s => s.Teacher)
            .ToArrayAsync();
    }

    public async Task<SubscriptionEntity[]?> GetSubscriptionsByTeacherIdAsync(Guid teacherId)
    {
        return await RockSchoolContext.Subscriptions
            .Where(s => s.TeacherId == teacherId)
            .Include(s => s.Student)
            .ToArrayAsync();
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