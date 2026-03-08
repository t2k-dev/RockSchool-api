using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Bands;

public class BandMemberService : IBandMemberService
{
    private readonly IBandMemberRepository _bandMemberRepository;

    public BandMemberService(IBandMemberRepository bandMemberRepository)
    {
        _bandMemberRepository = bandMemberRepository;
    }

    public async Task<BandMember?> GetByIdAsync(Guid id)
    {
        return await _bandMemberRepository.GetByIdAsync(id);
    }

    public async Task<BandMember[]> GetByBandIdAsync(Guid bandId)
    {
        return await _bandMemberRepository.GetByBandIdAsync(bandId);
    }

    public async Task<BandMember[]> GetByStudentIdAsync(Guid studentId)
    {
        return await _bandMemberRepository.GetByStudentIdAsync(studentId);
    }

    public async Task<Guid> AddStudentToBandAsync(BandMember bandMember)
    {
        return await _bandMemberRepository.AddAsync(bandMember);
    }

    public async Task UpdateBandMemberAsync(BandMember bandMember)
    {
        await _bandMemberRepository.UpdateAsync(bandMember);
    }

    public async Task DeleteBandMemberAsync(Guid id)
    {
        await _bandMemberRepository.DeleteAsync(id);
    }
}
