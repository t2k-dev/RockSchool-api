using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.ScheduleService;

public interface IScheduleService
{
    Task<ScheduleDto[]?> GetAllBySubscriptionIdAsync(Guid subscriptionId);
    Task<ScheduleDto[]?> GetAllSchedulesAsync();
    Task<Guid> AddScheduleAsync(ScheduleDto schedule);
}