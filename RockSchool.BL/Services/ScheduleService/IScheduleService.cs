using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.ScheduleService;

public interface IScheduleService
{
    Task<ScheduleDto[]?> GetAllSchedulesAsync();
    // Task AddScheduleAsync(AddScheduleServiceRequestDto requestDto);
}