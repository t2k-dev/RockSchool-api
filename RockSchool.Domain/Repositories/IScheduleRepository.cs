using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IScheduleRepository
{
    Task<Schedule[]> GetAllAsync();
    Task<Schedule[]?> GetAllBySubscriptionIdAsync(Guid subscriptionId);
    Task<Schedule?> GetByIdAsync(Guid scheduleId);
    Task<Guid> AddAsync(Schedule schedule);
    Task AddManyAsync(Schedule[] schedules);
    Task UpdateAsync(Schedule schedule);
    Task DeleteAsync(Guid id);
    Task DeleteBySubscriptionAsync(Guid subscriptionId);
}
