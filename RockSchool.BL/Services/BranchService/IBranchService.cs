using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.BranchService;

public interface IBranchService
{
    Task<BranchDto>? GetBranchByIdAsync(int id);
}