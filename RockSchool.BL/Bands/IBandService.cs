using RockSchool.BL.Models;
using RockSchool.BL.Models.Dtos;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;

namespace RockSchool.BL.Bands;

public interface IBandService
{
    Task<Band?> GetByIdAsync(Guid id);
    Task<Band[]> GetAllAsync();
    Task<Guid> AddBandAsync(string name, Guid teacherId, BandMemberDto[] members, ScheduleSlotDto[] schedules);
    Task AddBandMemberAsync(Guid bandId, Guid studentId, BandRoleId? bandRoleId);
    Task UpdateBandAsync(Band band);
    Task DeleteBandAsync(Guid id);
    Task<Band?> ActivateBandAsync(Guid id);
    Task<Band?> DeactivateBandAsync(Guid id);
}