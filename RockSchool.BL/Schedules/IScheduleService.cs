using RockSchool.BL.Models;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Schedules;

public interface IScheduleService
{
    Task<Schedule[]?> GetAllSchedulesAsync();
    Task<Guid> AddScheduleAsync(ScheduleSlotDto[] scheduleSlotDtos);
    Task AddSchedulesAsync(Schedule[] schedules);
}