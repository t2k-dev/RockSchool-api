using RockSchool.BL.Dtos.Service.Requests.DisciplineService;
using RockSchool.BL.Dtos.Service.Responses;

namespace RockSchool.BL.Services.DisciplineService;

public interface IDisciplineService
{
    Task AddDisciplineAsync(AddDisciplineServiceRequestDto requestDto);
    Task<DisciplineDto[]?> GetAllDisciplinesAsync();
    Task UpdateDisciplineAsync(UpdateDisciplineServiceRequestDto serviceRequestDto);
    Task DeleteDisciplineAsync(DeleteDisciplineServiceRequestDto requestDto);
}