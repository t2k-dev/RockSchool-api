using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Schedules
{
    public interface IScheduledWorkingPeriodsRepository
    {
        Task AddRangeAsync(List<ScheduledWorkingPeriod> scheduledWorkingPeriods);
    }
}
