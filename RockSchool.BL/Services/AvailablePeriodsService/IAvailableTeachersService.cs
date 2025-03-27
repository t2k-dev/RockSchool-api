using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.AvailablePeriodsService;

public interface IAvailableTeachersService
{
    Task<AvailableTeachersDto?> GetAvailableTeachersAsync(int disciplineId, int branchId, int studentAge);
}