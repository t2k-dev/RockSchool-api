using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

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
        var scheduleEntities = await _scheduleRepository.GetAllBySubscriptionIdAsync(subscriptionId);

        var schedules = scheduleEntities.Select(s => new Schedule
        {
            ScheduleId = s.ScheduleId,
            Subscription = s.Subscription.ToDto(),
            WeekDay = s.WeekDay,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            RoomId = s.RoomId,

        }).ToArray();

        return schedules;
    }

    public async Task<Schedule[]?> GetAllSchedulesAsync()
    {
        var schedules = await _scheduleRepository.GetAllAsync();

        if (schedules == null || !schedules.Any())
            return null;

        var scheduleDtos = schedules.Select(s => new Schedule
        {
            ScheduleId = s.ScheduleId,
            Subscription = s.Subscription.ToDto(),
            WeekDay = s.WeekDay,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            RoomId = s.RoomId,

        }).ToArray();
        
        return scheduleDtos;
    }

    public async Task<Guid> AddScheduleAsync(Schedule schedule)
    {
        var scheduleEntity = new ScheduleEntity
        {
            SubscriptionId = schedule.SubscriptionId,
            WeekDay = schedule.WeekDay,
            StartTime = schedule.StartTime,
            EndTime = schedule.EndTime,
            RoomId = schedule.RoomId,
        };

        return await _scheduleRepository.AddAsync(scheduleEntity);
    }
}