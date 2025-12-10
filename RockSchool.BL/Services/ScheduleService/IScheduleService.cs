using RockSchool.BL.Models;

namespace RockSchool.BL.Services.ScheduleService;

public interface IScheduleService
{
    Task<Schedule[]?> GetAllBySubscriptionIdAsync(Guid subscriptionId);
    Task<Schedule[]?> GetAllSchedulesAsync();
    Task<Guid> AddScheduleAsync(Schedule schedule);
    Task AddSchedulesAsync(Schedule[] schedules);
    Task DeleteBySubscriptionAsync(Guid subscriptionId);
}