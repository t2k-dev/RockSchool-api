using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IBandRepository
{
    Task<Band[]> GetAllAsync();
    Task<Band[]> GetActiveByBranchIdAsync(int branchId);
    Task<Band?> GetByIdAsync(Guid id);
    Task<Band?> GetByIdWithScheduleAsync(Guid id);
    Task<Band[]> GetByTeacherIdAsync(Guid teacherId);
    Task<Guid> AddAsync(Band band);
    void Update(Band band);
    Task DeleteAsync(Guid id);
}
