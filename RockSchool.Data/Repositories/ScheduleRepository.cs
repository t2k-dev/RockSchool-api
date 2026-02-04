using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Repositories;

public class ScheduleRepository : BaseRepository
{
    public ScheduleRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
    {
    }

    public async Task<Schedule[]> GetAllAsync()
    {
        return await RockSchoolContext.Schedules.ToArrayAsync();
    }

    public async Task<Schedule[]?> GetAllBySubscriptionIdAsync(Guid subscriptionId)
    {
        return await RockSchoolContext.Schedules.Where(s => s.SubscriptionId == subscriptionId).ToArrayAsync();
    }

    public async Task<Schedule?> GetByIdAsync(Guid scheduleId)
    {
        return await RockSchoolContext.Schedules.FirstOrDefaultAsync(s => s.ScheduleId == scheduleId);
    }

    public async Task<Guid> AddAsync(Schedule schedule)
    {
        await RockSchoolContext.Schedules.AddAsync(schedule);
        await RockSchoolContext.SaveChangesAsync();
        return schedule.ScheduleId;
    }

    public async Task AddManyAsync(Schedule[] schedules)
    {
        await RockSchoolContext.Schedules.AddRangeAsync(schedules);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Schedule schedule)
    {
        RockSchoolContext.Schedules.Update(schedule);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var schedule = await RockSchoolContext.Schedules.FirstOrDefaultAsync(s => s.ScheduleId == id);
        if (schedule != null)
        {
            RockSchoolContext.Schedules.Remove(schedule);
            await RockSchoolContext.SaveChangesAsync();
        }
    }

    public async Task DeleteBySubscriptionAsync(Guid subscriptionId)
    {
        var schedules = await RockSchoolContext.Schedules.Where(s => s.SubscriptionId == subscriptionId).ToArrayAsync();

        RockSchoolContext.Schedules.RemoveRange(schedules);
        await RockSchoolContext.SaveChangesAsync();
    }
}