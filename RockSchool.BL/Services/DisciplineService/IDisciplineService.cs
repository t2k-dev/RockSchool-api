
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.DisciplineService;

public interface IDisciplineService
{
    Task AddDisciplineAsync(string disciplineName);
    Task<Discipline[]?> GetAllDisciplinesAsync();
    Task UpdateDisciplineAsync(Discipline disciplineDto);
    Task DeleteDisciplineAsync(int id);
    Task<Discipline?> GetDisciplineByIdAsync(int id);
}