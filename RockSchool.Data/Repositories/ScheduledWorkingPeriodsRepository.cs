using RockSchool.Data.Data;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Repositories;

public class ScheduledWorkingPeriodsRepository : BaseRepository
{
    public ScheduledWorkingPeriodsRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
    {
    }

    public async Task AddRangeAsync(List<ScheduledWorkingPeriod> scheduledWorkingPeriods)
    {
        await RockSchoolContext.AddRangeAsync(scheduledWorkingPeriods);
        await RockSchoolContext.SaveChangesAsync();
    }
}