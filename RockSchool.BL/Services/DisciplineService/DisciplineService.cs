using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.DisciplineService;

public class DisciplineService : IDisciplineService
{
    private readonly DisciplineRepository _disciplineRepository;

    public DisciplineService(DisciplineRepository disciplineRepository)
    {
        _disciplineRepository = disciplineRepository;
    }

    public async Task AddDisciplineAsync(string disciplineName)
    {
        var newDiscipline = Discipline.Create(disciplineName);
        await _disciplineRepository.AddAsync(newDiscipline);
    }

    public async Task<Discipline[]?> GetAllDisciplinesAsync()
    {
        var disciplines = await _disciplineRepository.GetAllAsync();

        if (disciplines == null || !disciplines.Any())
            return null;

        return disciplines;
    }

    public async Task UpdateDisciplineAsync(Discipline disciplineDto)
    {
        throw new NotImplementedException();
        /*
        var discipline = await _disciplineRepository.GetByIdAsync(disciplineDto.DisciplineId);

        if (discipline == null)
            throw new ArgumentNullException("DisciplineEntity not found.");

        discipline.

        discipline.Name = disciplineDto.Name;
        discipline.IsActive = disciplineDto.IsActive;

        await _disciplineRepository.UpdateAsync(discipline);*/
    }


    public async Task DeleteDisciplineAsync(int id)
    {
        var discipline = await _disciplineRepository.GetByIdAsync(id);

        if (discipline == null)
            throw new NullReferenceException("DisciplineEntity not found.");

        await _disciplineRepository.DeleteAsync(discipline);
    }

    public async Task<Discipline?> GetDisciplineByIdAsync(int id)
    {
        var disciplineEntity = await _disciplineRepository.GetByIdAsync(id);

        if (disciplineEntity == null)
            return null;

        return disciplineEntity;
    }
}