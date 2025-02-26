using RockSchool.BL.Dtos;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.BranchService;

public class BranchService : IBranchService
{
    private readonly BranchRepository _branchRepository;

    public BranchService(BranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<BranchDto>? GetBranchByIdAsync(int id)
    {
        var branchEntity = await _branchRepository.GetByIdAsync(id);

        if (branchEntity == null)
            return null;

        return new BranchDto
        {
            BranchId = branchEntity.BranchId,
            Name = branchEntity.Name,
            Phone = branchEntity.Phone,
            Address = branchEntity.Address,
            Rooms = branchEntity.Rooms
        };
    }
}