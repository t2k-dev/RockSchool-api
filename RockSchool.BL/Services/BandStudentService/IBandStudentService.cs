using RockSchool.BL.Models;

namespace RockSchool.BL.Services.BandStudentService;

public interface IBandStudentService
{
    Task<BandStudent?> GetByIdAsync(Guid id);
    Task<BandStudent[]> GetByBandIdAsync(Guid bandId);
    Task<BandStudent[]> GetByStudentIdAsync(Guid studentId);
    Task<Guid> AddStudentToBandAsync(BandStudent bandStudent);
    Task UpdateBandStudentAsync(BandStudent bandStudent);
    Task RemoveStudentFromBandAsync(Guid bandId, Guid studentId);
    Task DeleteBandStudentAsync(Guid id);
}