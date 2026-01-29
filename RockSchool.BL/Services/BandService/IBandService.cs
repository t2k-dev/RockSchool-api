using RockSchool.BL.Models;
using RockSchool.BL.Models.Dtos;

namespace RockSchool.BL.Services.BandService;

public interface IBandService
{
    Task<Band?> GetByIdAsync(Guid id);
    Task<Band[]> GetAllAsync();
    Task<Band[]> GetByTeacherIdAsync(Guid teacherId);
    Task<Guid> AddBandAsync(string name, Guid teacherId, BandMember[] members, Schedule[] schedules);
    Task UpdateBandAsync(Band band);
    Task DeleteBandAsync(Guid id);
}