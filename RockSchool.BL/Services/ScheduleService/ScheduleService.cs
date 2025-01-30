using RockSchool.BL.Dtos.Service.Requests.ScheduleService;
using RockSchool.BL.Dtos.Service.Responses;
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

    public async Task<ScheduleDto[]?> GetAllSchedulesAsync()
    {
        var schedules = await _scheduleRepository.GetAllAsync();

        if (schedules == null || !schedules.Any())
            return null;

        var scheduleDtos = schedules.Select(s => new ScheduleDto
        {
            ScheduleId = s.ScheduleId,
            StudentId = s.StudentId,
            StudentEntity = s.StudentEntity,
            WeekDay = s.WeekDay,
            StartTime = s.StartTime,
            Duration = s.Duration,
            DisciplineId = s.DisciplineId,
            DisciplineEntity = s.DisciplineEntity
        }).ToArray();

        return scheduleDtos;
    }

    public async Task AddScheduleAsync(AddScheduleServiceRequestDto requestDto)
    {
        var schedule = new ScheduleEntity
        {
            StudentId = requestDto.StudentId,
            WeekDay = requestDto.WeekDay,
            StartTime = requestDto.StartTime,
            Duration = requestDto.Duration,
            DisciplineId = requestDto.DisciplineId
        };

        await _scheduleRepository.AddAsync(schedule);
        var savedSchedule = await _scheduleRepository.GetByIdAsync(schedule.ScheduleId);

        if (savedSchedule == null)
            throw new InvalidOperationException("Failed to add schedule.");
    }
}