using AutoMapper;
using RockSchool.BL.Dtos.Service.Requests.DisciplineService;
using RockSchool.BL.Dtos.Service.Responses;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.DisciplineService;

public class DisciplineService : IDisciplineService
{
    private readonly DisciplineRepository _disciplineRepository;
    protected readonly IMapper _mapper;

    public DisciplineService(DisciplineRepository disciplineRepository, IMapper mapper)
    {
        _disciplineRepository = disciplineRepository;
        _mapper = mapper;
    }

    public async Task AddDisciplineAsync(
        AddDisciplineServiceRequestDto requestDto)
    {
        var discipline = _mapper.Map<DisciplineEntity>(requestDto);
        await _disciplineRepository.AddAsync(discipline);
    }

    public async Task<DisciplineDto[]?> GetAllDisciplinesAsync()
    {
        var disciplines = await _disciplineRepository.GetAllAsync();
        var disciplineDtos = _mapper.Map<DisciplineDto[]>(disciplines);

        return disciplineDtos;
    }

    public async Task UpdateDisciplineAsync(
        UpdateDisciplineServiceRequestDto serviceRequestDto)
    {
        var discipline = await _disciplineRepository.GetByIdAsync(serviceRequestDto.Id);

        if (discipline == null)
            throw new ArgumentNullException("DisciplineEntity not found.");

        var updatedDiscipline = _mapper.Map<DisciplineEntity>(serviceRequestDto);
        await _disciplineRepository.UpdateAsync(updatedDiscipline);
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