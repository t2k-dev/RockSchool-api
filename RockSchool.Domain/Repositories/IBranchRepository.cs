using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IBranchRepository
{
    Task<Branch?> GetByIdAsync(int id);
}
