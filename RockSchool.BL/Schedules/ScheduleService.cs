using RockSchool.BL.Models;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Schedules;

public class ScheduleService(IScheduleRepository scheduleRepository) : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository = scheduleRepository;

    public async Task<Schedule[]?> GetAllSchedulesAsync()
    {
        var schedules = await _scheduleRepository.GetAllAsync();

        if (schedules == null || !schedules.Any())
            return null;
        
        return schedules;
    }

    public async Task<Guid> AddScheduleAsync(ScheduleSlotDto[] scheduleSlotDtos)
    {
        var schedule = Schedule.Create($"Schedule {DateTime.Now}");

        foreach (var scheduleSlotDto in scheduleSlotDtos)
        {
            var slot = ScheduleSlot.Create(
                schedule.ScheduleId,
                scheduleSlotDto.RoomId,
                scheduleSlotDto.WeekDay,
                scheduleSlotDto.StartTime,
                scheduleSlotDto.EndTime
            );
            schedule.AddScheduleSlot(slot);
        }

        return await scheduleRepository.AddAsync(schedule);
    }

    public async Task AddSchedulesAsync(Schedule[] schedules)
    {
        await _scheduleRepository.AddManyAsync(schedules);
    }

}