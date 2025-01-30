using RockSchool.BL.Dtos.Service.Requests.DisciplineService;
using RockSchool.BL.Dtos.Service.Responses;
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

    public async Task AddDisciplineAsync(
        AddDisciplineServiceRequestDto requestDto)
    {
        var discipline = new DisciplineEntity
        {
            Id = requestDto.Id,
            DisciplineName = requestDto.DisciplineName,
            Teachers = requestDto.Teachers,
            IsActive = requestDto.IsActive
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
            Id = d.Id,
            DisciplineName = d.DisciplineName,
            Teachers = d.Teachers, // Be careful with navigation properties
            IsActive = d.IsActive
        }).ToArray();

        return disciplineDtos;
    }

    public async Task UpdateDisciplineAsync(UpdateDisciplineServiceRequestDto serviceRequestDto)
    {
        var discipline = await _disciplineRepository.GetByIdAsync(serviceRequestDto.Id);

        if (discipline == null)
            throw new ArgumentNullException("DisciplineEntity not found.");

        discipline.DisciplineName = serviceRequestDto.DisciplineName;
        discipline.IsActive = serviceRequestDto.IsActive;

        await _disciplineRepository.UpdateAsync(discipline);
    }


    public async Task DeleteDisciplineAsync(
        DeleteDisciplineServiceRequestDto requestDto)
    {
        var discipline = await _disciplineRepository.GetByIdAsync(requestDto.Id);

        if (discipline == null)
            throw new NullReferenceException("DisciplineEntity not found.");

        await _disciplineRepository.DeleteAsync(discipline);
    }
}