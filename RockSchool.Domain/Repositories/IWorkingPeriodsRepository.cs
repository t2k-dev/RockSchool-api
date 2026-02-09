using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IWorkingPeriodsRepository
{
    Task<WorkingPeriod[]?> GetWorkingPeriods(Guid teacherId);
    Task DeleteWorkingPeriodsByTeacherId(Guid teacherId);
    void DeleteRange(WorkingPeriod[] periods);
    Task AddRangeAsync(WorkingPeriod[] periods);
}
