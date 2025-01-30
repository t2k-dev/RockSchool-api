using RockSchool.BL.Dtos.Service.Requests.ScheduleService;
using RockSchool.BL.Dtos.Service.Responses;

namespace RockSchool.BL.Services.ScheduleService;

public interface IScheduleService
{
    Task<ScheduleDto[]?> GetAllSchedulesAsync();
    Task AddScheduleAsync(AddScheduleServiceRequestDto requestDto);
}