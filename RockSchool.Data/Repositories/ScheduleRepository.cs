using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Repositories;

public class ScheduleRepository
{
    private readonly RockSchoolContext _rockSchoolContext;

    public ScheduleRepository(RockSchoolContext rockSchoolContext)
    {
        _rockSchoolContext = rockSchoolContext;
    }

    public async Task<ScheduleEntity[]> GetAllAsync()
    {
        return await _rockSchoolContext.Schedules.ToArrayAsync();
    }

    public async Task<ScheduleEntity[]?> GetAllByStudentIdAsync(Guid studentId)
    {
        return await _rockSchoolContext.Schedules.Where(s => s.Subscription.StudentId == studentId).ToArrayAsync();
    }

    public async Task<ScheduleEntity?> GetByIdAsync(Guid scheduleId)
    {
        return await _rockSchoolContext.Schedules.FirstOrDefaultAsync(s => s.ScheduleId == scheduleId);
    }

    public async Task AddAsync(ScheduleEntity scheduleEntity)
    {
        await _rockSchoolContext.Schedules.AddAsync(scheduleEntity);
        await _rockSchoolContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(ScheduleEntity scheduleEntity)
    {
        _rockSchoolContext.Schedules.Update(scheduleEntity);
        await _rockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var schedule = await _rockSchoolContext.Schedules.FirstOrDefaultAsync(s => s.ScheduleId == id);
        if (schedule != null)
        {
            _rockSchoolContext.Schedules.Remove(schedule);
            await _rockSchoolContext.SaveChangesAsync();
        }
    }
}