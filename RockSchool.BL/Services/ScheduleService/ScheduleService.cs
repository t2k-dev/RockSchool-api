using RockSchool.BL.Dtos;
using RockSchool.BL.Helpers;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.ScheduleService;

public class ScheduleService : IScheduleService
{
    private readonly ScheduleRepository _scheduleRepository;

    public ScheduleService(ScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<ScheduleDto[]?> GetAllSchedulesAsync()
    {
        var schedules = await _scheduleRepository.GetAllAsync();

        if (schedules == null || !schedules.Any())
            return null;

        var scheduleDtos = schedules.Select(s => new ScheduleDto
        {
            ScheduleId = s.ScheduleId,
            Subscription = s.Subscription.ToDto(),
            WeekDay = s.WeekDay,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
        }).ToArray();
        
        return scheduleDtos;
    }



    public async Task AddScheduleAsync(ScheduleDto requestDto)
    {
        // TODO: we need to add discipline here !! FFS
        // var schedule = new ScheduleEntity
        // {
        //     SubscriptionId = requestDto.,
        //     WeekDay = requestDto.WeekDay,
        //     StartTime = requestDto.StartTime,
        //     EndDate = requestDto.EndDate,
        //     DisciplineId = requestDto.DisciplineId
        // };

        // await _scheduleRepository.AddSubscriptionAsync(schedule);
        // var savedSchedule = await _scheduleRepository.GetByIdAsync(schedule.ScheduleId);
        //
        // if (savedSchedule == null)
        //     throw new InvalidOperationException("Failed to add schedule.");
    }
}