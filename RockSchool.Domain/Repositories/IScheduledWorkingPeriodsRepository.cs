using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IScheduledWorkingPeriodsRepository
{
    Task AddRangeAsync(List<ScheduledWorkingPeriod> scheduledWorkingPeriods);
    Task DeleteForTeacherAfter(Guid teacherId, DateTime deleteAfter);
}
