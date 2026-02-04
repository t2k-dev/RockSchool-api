
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.ScheduledWorkingPeriodsService
{
    public interface IScheduledWorkingPeriodsService
    {
        Task AddPeriods(Guid teacherId, DateTime startDate, int months, List<WorkingPeriod> workingPeriodEntities);
    }
}
