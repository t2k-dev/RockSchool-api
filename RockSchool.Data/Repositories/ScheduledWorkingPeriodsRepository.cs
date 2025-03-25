using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Repositories;

public class ScheduledWorkingPeriodsRepository : BaseRepository
{
    public ScheduledWorkingPeriodsRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
    {
    }

    public async Task AddRangeAsync(List<ScheduledWorkingPeriodEntity> scheduledWorkingPeriodEntities)
    {
        await RockSchoolContext.AddRangeAsync(scheduledWorkingPeriodEntities);
        await RockSchoolContext.SaveChangesAsync();
    }
}