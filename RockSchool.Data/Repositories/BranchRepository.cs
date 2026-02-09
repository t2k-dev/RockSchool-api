using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.Data.Repositories;

public class BranchRepository : BaseRepository, IBranchRepository
{
    public BranchRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
    {
    }

    public async Task<Branch?> GetByIdAsync(int id)
    {
        return await  RockSchoolContext.Branches.FirstOrDefaultAsync(b => b.BranchId == id);
    }
}