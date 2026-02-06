using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Branches
{
    public interface IBranchRepository
    {
        Task<Branch?> GetByIdAsync(int id);
    }
}
