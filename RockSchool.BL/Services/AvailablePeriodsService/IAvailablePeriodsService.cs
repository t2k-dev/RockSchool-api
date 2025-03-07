using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.AvailablePeriodsService;

public interface IAvailablePeriodsService
{
    Task<AvailablePeriodsDto?> GetAvailablePeriodsAsync(int disciplineId, int branchId);
}