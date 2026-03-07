using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.BandMemberService;

public interface IBandMemberService
{
    Task<BandMember?> GetByIdAsync(Guid id);
    Task<BandMember[]> GetByBandIdAsync(Guid bandId);
    Task<BandMember[]> GetByStudentIdAsync(Guid studentId);
    Task<Guid> AddStudentToBandAsync(BandMember bandMember);
    Task UpdateBandMemberAsync(BandMember bandMember);
    Task DeleteBandMemberAsync(Guid id);
}
