using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Repositories;

public class ScheduleRepository : BaseRepository
{
    public ScheduleRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
    {
    }

    public async Task<ScheduleEntity[]> GetAllAsync()
    {
        return await RockSchoolContext.Schedules.ToArrayAsync();
    }

    public async Task<ScheduleEntity[]?> GetAllBySubscriptionIdAsync(Guid subscriptionId)
    {
        return await RockSchoolContext.Schedules.Where(s => s.SubscriptionId == subscriptionId).ToArrayAsync();
    }

    public async Task<ScheduleEntity?> GetByIdAsync(Guid scheduleId)
    {
        return await RockSchoolContext.Schedules.FirstOrDefaultAsync(s => s.ScheduleId == scheduleId);
    }

    public async Task<Guid> AddAsync(ScheduleEntity scheduleEntity)
    {
        await RockSchoolContext.Schedules.AddAsync(scheduleEntity);
        await RockSchoolContext.SaveChangesAsync();
        return scheduleEntity.ScheduleId;
    }

    public async Task UpdateAsync(ScheduleEntity scheduleEntity)
    {
        RockSchoolContext.Schedules.Update(scheduleEntity);
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


}