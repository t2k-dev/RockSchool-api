using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Repositories;

public class BranchRepository : BaseRepository
{
    public BranchRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
    {
    }

    public async Task<BranchEntity?> GetByIdAsync(int id)
    {
        return await  RockSchoolContext.Branches.FirstOrDefaultAsync(b => b.BranchId == id);
    }
}