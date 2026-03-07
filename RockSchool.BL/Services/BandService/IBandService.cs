using RockSchool.BL.Models;
using RockSchool.BL.Models.Dtos;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.BandService;

public interface IBandService
{
    Task<Band?> GetByIdAsync(Guid id);
    Task<Band[]> GetAllAsync();
    Task<Guid> AddBandAsync(string name, Guid teacherId, BandMemberDto[] members, ScheduleSlotDto[] schedules);
    Task UpdateBandAsync(Band band);
    Task DeleteBandAsync(Guid id);
    Task<Band?> ActivateBandAsync(Guid id);
    Task<Band?> DeactivateBandAsync(Guid id);
}