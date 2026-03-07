using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IBandMemberRepository
{
    Task<BandMember?> GetByIdAsync(Guid id);
    Task<BandMember[]> GetByBandIdAsync(Guid bandId);
    Task<BandMember[]> GetByStudentIdAsync(Guid studentId);
    Task<Guid> AddAsync(BandMember bandMember);
    Task UpdateAsync(BandMember bandMember);
    Task DeleteAsync(Guid id);
}
