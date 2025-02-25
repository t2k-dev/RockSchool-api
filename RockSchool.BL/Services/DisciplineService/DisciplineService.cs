using RockSchool.BL.Dtos;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.DisciplineService;

public class DisciplineService : IDisciplineService
{
    private readonly DisciplineRepository _disciplineRepository;

    public DisciplineService(DisciplineRepository disciplineRepository)
    {
        _disciplineRepository = disciplineRepository;
    }

    public async Task AddDisciplineAsync(DisciplineDto disciplineDto)
    {
        var discipline = new DisciplineEntity
        {
            DisciplineId = disciplineDto.DisciplineId,
            Name = disciplineDto.Name,
            Teachers = disciplineDto.Teachers,
            IsActive = disciplineDto.IsActive
        };

        await _disciplineRepository.AddAsync(discipline);
    }

    public async Task<DisciplineDto[]?> GetAllDisciplinesAsync()
    {
        var disciplines = await _disciplineRepository.GetAllAsync();

        if (disciplines == null || !disciplines.Any())
            return null;

        var disciplineDtos = disciplines.Select(d => new DisciplineDto
        {
            DisciplineId = d.DisciplineId,
            Name = d.Name,
            Teachers = d.Teachers, // Be careful with navigation properties
            IsActive = d.IsActive
        }).ToArray();

        return disciplineDtos;
    }

    public async Task UpdateDisciplineAsync(DisciplineDto disciplineDto)
    {
        var discipline = await _disciplineRepository.GetByIdAsync(disciplineDto.DisciplineId);

        if (discipline == null)
            throw new ArgumentNullException("DisciplineEntity not found.");

        discipline.Name = disciplineDto.Name;
        discipline.IsActive = disciplineDto.IsActive;

        await _disciplineRepository.UpdateAsync(discipline);
    }


    public async Task DeleteDisciplineAsync(int id)
    {
        var discipline = await _disciplineRepository.GetByIdAsync(id);

        if (discipline == null)
            throw new NullReferenceException("DisciplineEntity not found.");

        await _disciplineRepository.DeleteAsync(discipline);
    }
}