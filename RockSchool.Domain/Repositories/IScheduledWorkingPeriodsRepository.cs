using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Repositories;

public interface IScheduledWorkingPeriodsRepository
{
    Task AddRangeAsync(List<ScheduledWorkingPeriod> scheduledWorkingPeriods);
    void DeleteForTeacherAfter(Guid teacherId, DateTime deleteAfter);
}
