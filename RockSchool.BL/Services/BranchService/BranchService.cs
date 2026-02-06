using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Branches;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.BranchService;

public class BranchService : IBranchService
{
    private readonly IBranchRepository _branchRepository;

    public BranchService(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<Branch>? GetBranchByIdAsync(int id)
    {
        return await _branchRepository.GetByIdAsync(id);
    }
}