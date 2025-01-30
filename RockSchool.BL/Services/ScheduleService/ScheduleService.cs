using AutoMapper;
using RockSchool.BL.Dtos.Service.Requests.ScheduleService;
using RockSchool.BL.Dtos.Service.Responses;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.ScheduleService;

public class ScheduleService : IScheduleService
{
    private ScheduleRepository _scheduleRepository;
    protected readonly IMapper _mapper;

    public ScheduleService(ScheduleRepository scheduleRepository, IMapper mapper)
    {
        _scheduleRepository = scheduleRepository;
        _mapper = mapper;
    }

    public async Task<ScheduleDto[]?> GetAllSchedulesAsync()
    {
        var schedules = await _scheduleRepository.GetAllAsync();
        var scheduleDtos = _mapper.Map<ScheduleDto[]>(schedules);

        return scheduleDtos;
    }

    public async Task AddScheduleAsync(
        AddScheduleServiceRequestDto requestDto)
    {
        var schedule = _mapper.Map<ScheduleEntity>(requestDto);
        await _scheduleRepository.AddAsync(schedule);
        var savedSchedule = await _scheduleRepository.GetByIdAsync(schedule.ScheduleId);

        if (savedSchedule == null)
            throw new InvalidOperationException("Failed to add schedule.");
    }
}