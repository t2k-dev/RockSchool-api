using RockSchool.BL.Models;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.BranchService;

public interface IBranchService
{
    Task<Branch>? GetBranchByIdAsync(int id);
}