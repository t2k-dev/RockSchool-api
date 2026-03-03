
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.ScheduleService;

public interface IScheduleService
{
    Task<Schedule[]?> GetAllSchedulesAsync();
    Task<Guid> AddScheduleAsync(Schedule schedule);
    Task AddSchedulesAsync(Schedule[] schedules);
}