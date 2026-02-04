using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;

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
        return await _bandStudentRepository.GetByIdAsync(id);
    }

    public async Task<BandStudent[]> GetByBandIdAsync(Guid bandId)
    {
        return await _bandStudentRepository.GetByBandIdAsync(bandId);
    }

    public async Task<BandStudent[]> GetByStudentIdAsync(Guid studentId)
    {
        return await _bandStudentRepository.GetByStudentIdAsync(studentId);
    }

    public async Task<Guid> AddStudentToBandAsync(BandStudent bandStudent)
    {
        throw new NotImplementedException();
        /*
        bandStudent.BandStudentId = Guid.NewGuid();
        var entity = bandStudent.ToEntity();
        return await _bandStudentRepository.AddAsync(entity);*/
    }

    public async Task UpdateBandStudentAsync(BandStudent bandStudent)
    {
        await _bandStudentRepository.UpdateAsync(bandStudent);
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