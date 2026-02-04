using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.ScheduleService;

public class ScheduleService : IScheduleService
{
    private readonly ScheduleRepository _scheduleRepository;

    public ScheduleService(ScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<Schedule[]?> GetAllBySubscriptionIdAsync(Guid subscriptionId)
    {
        return await _scheduleRepository.GetAllBySubscriptionIdAsync(subscriptionId);
    }

    public async Task<Schedule[]?> GetAllSchedulesAsync()
    {
        var schedules = await _scheduleRepository.GetAllAsync();

        if (schedules == null || !schedules.Any())
            return null;
        
        return schedules;
    }

    public async Task<Guid> AddScheduleAsync(Schedule schedule)
    {
        return await _scheduleRepository.AddAsync(schedule);
    }

    public async Task AddSchedulesAsync(Schedule[] schedules)
    {
        await _scheduleRepository.AddManyAsync(schedules);
    }

    public async Task DeleteBySubscriptionAsync(Guid subscriptionId)
    {
        await _scheduleRepository.DeleteBySubscriptionAsync(subscriptionId);
    }
}