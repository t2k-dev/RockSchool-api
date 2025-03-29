using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.AvailableTeachersService;

public interface IAvailableTeachersService
{
    Task<AvailableTeachersDto?> GetAvailableTeachersAsync(int disciplineId, int branchId, int studentAge);
}