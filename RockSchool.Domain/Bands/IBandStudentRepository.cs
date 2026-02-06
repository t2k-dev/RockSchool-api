using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Bands
{
    public interface IBandStudentRepository
    {
        Task<BandStudent?> GetByIdAsync(Guid id);
        Task<BandStudent[]> GetByBandIdAsync(Guid bandId);
        Task<BandStudent[]> GetByStudentIdAsync(Guid studentId);
        Task<Guid> AddAsync(BandStudent bandStudent);
        Task UpdateAsync(BandStudent bandStudent);
        Task DeleteAsync(Guid id);
        Task RemoveStudentFromBandAsync(Guid bandId, Guid studentId);
    }
}
