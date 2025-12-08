using RockSchool.BL.Models;

namespace RockSchool.BL.Services.DisciplineService;

public interface IDisciplineService
{
    Task AddDisciplineAsync(Discipline disciplineDto);
    Task<Discipline[]?> GetAllDisciplinesAsync();
    Task UpdateDisciplineAsync(Discipline disciplineDto);
    Task DeleteDisciplineAsync(int id);
    Task<Discipline?> GetDisciplineByIdAsync(int id);
}