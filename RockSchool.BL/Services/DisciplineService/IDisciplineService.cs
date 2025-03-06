using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.DisciplineService;

public interface IDisciplineService
{
    Task AddDisciplineAsync(DisciplineDto disciplineDto);
    Task<DisciplineDto[]?> GetAllDisciplinesAsync();
    Task UpdateDisciplineAsync(DisciplineDto disciplineDto);
    Task DeleteDisciplineAsync(int id);
    Task<DisciplineDto?> GetDisciplineByIdAsync(int id);
}