using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.BandStudentService;

public class BandStudentService : IBandStudentService
{
    private readonly BandStudentRepository _bandStudentRepository;

    public BandStudentService(BandStudentRepository bandStudentRepository)
    {
        _bandStudentRepository = bandStudentRepository;
    }

    public async Task<BandStudent?> GetByIdAsync(Guid id)
    {
        var entity = await _bandStudentRepository.GetByIdAsync(id);
        return entity?.ToModel();
    }

    public async Task<BandStudent[]> GetByBandIdAsync(Guid bandId)
    {
        var entities = await _bandStudentRepository.GetByBandIdAsync(bandId);
        return entities.ToModel();
    }

    public async Task<BandStudent[]> GetByStudentIdAsync(Guid studentId)
    {
        var entities = await _bandStudentRepository.GetByStudentIdAsync(studentId);
        return entities.ToModel();
    }

    public async Task<Guid> AddStudentToBandAsync(BandStudent bandStudent)
    {
        bandStudent.BandStudentId = Guid.NewGuid();
        var entity = bandStudent.ToEntity();
        return await _bandStudentRepository.AddAsync(entity);
    }

    public async Task UpdateBandStudentAsync(BandStudent bandStudent)
    {
        var entity = bandStudent.ToEntity();
        await _bandStudentRepository.UpdateAsync(entity);
    }

    public async Task RemoveStudentFromBandAsync(Guid bandId, Guid studentId)
    {
        await _bandStudentRepository.RemoveStudentFromBandAsync(bandId, studentId);
    }

    public async Task DeleteBandStudentAsync(Guid id)
    {
        await _bandStudentRepository.DeleteAsync(id);
    }
}