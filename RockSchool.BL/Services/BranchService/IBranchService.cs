using RockSchool.BL.Models;

namespace RockSchool.BL.Services.BranchService;

public interface IBranchService
{
    Task<Branch>? GetBranchByIdAsync(int id);
}