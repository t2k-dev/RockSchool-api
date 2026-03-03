using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.Data.Repositories;

public class ScheduleSlotRepository : BaseRepository, IScheduleSlotRepository
{
    public ScheduleSlotRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
    {
    }

    public async Task<ScheduleSlot[]> GetAllAsync()
    {
        return await RockSchoolContext.ScheduleSlots.ToArrayAsync();
    }

    public async Task<ScheduleSlot[]?> GetByScheduleIdAsync(Guid scheduleId)
    {
        return await RockSchoolContext.ScheduleSlots
            .Where(s => s.ScheduleId == scheduleId)
            .ToArrayAsync();
    }

    public async Task<ScheduleSlot?> GetByIdAsync(Guid slotId)
    {
        return await RockSchoolContext.ScheduleSlots
            .Include(s => s.Room)
            .FirstOrDefaultAsync(s => s.ScheduleSlotId == slotId);
    }

    public async Task<Guid> AddAsync(ScheduleSlot slot)
    {
        await RockSchoolContext.ScheduleSlots.AddAsync(slot);
        return slot.ScheduleSlotId;
    }

    public async Task AddManyAsync(ScheduleSlot[] slots)
    {
        await RockSchoolContext.ScheduleSlots.AddRangeAsync(slots);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(ScheduleSlot slot)
    {
        RockSchoolContext.ScheduleSlots.Update(slot);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var slot = await RockSchoolContext.ScheduleSlots.FirstOrDefaultAsync(s => s.ScheduleSlotId == id);
        if (slot != null)
        {
            RockSchoolContext.ScheduleSlots.Remove(slot);
            await RockSchoolContext.SaveChangesAsync();
        }
    }

    public async Task DeleteByScheduleAsync(Guid scheduleId)
    {
        var slots = await RockSchoolContext.ScheduleSlots
            .Where(s => s.ScheduleId == scheduleId)
            .ToArrayAsync();

        if (slots.Any())
        {
            RockSchoolContext.ScheduleSlots.RemoveRange(slots);
            await RockSchoolContext.SaveChangesAsync();
        }
    }
}
