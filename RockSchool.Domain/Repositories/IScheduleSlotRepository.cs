using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IScheduleSlotRepository
{
    Task<ScheduleSlot[]> GetAllAsync();
    Task<ScheduleSlot[]?> GetByScheduleIdAsync(Guid scheduleId);
    Task<ScheduleSlot?> GetByIdAsync(Guid slotId);
    Task<Guid> AddAsync(ScheduleSlot slot);
    Task AddManyAsync(ScheduleSlot[] slots);
    Task UpdateAsync(ScheduleSlot slot);
    Task DeleteAsync(Guid id);
    Task DeleteByScheduleAsync(Guid scheduleId);
}
