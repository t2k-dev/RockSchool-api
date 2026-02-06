using RockSchool.Domain.Entities;

namespace RockSchool.Domain.WorkingPeriods
{
    public interface IWorkingPeriodsRepository
    {
        Task<WorkingPeriod[]?> GetWorkingPeriods(Guid teacherId);
    }
}
