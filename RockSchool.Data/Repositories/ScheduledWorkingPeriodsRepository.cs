using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.Data.Repositories;

public class ScheduledWorkingPeriodsRepository : BaseRepository, IScheduledWorkingPeriodsRepository
{
    public ScheduledWorkingPeriodsRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
    {
    }

    public async Task AddRangeAsync(List<ScheduledWorkingPeriod> scheduledWorkingPeriods)
    {
        await RockSchoolContext.AddRangeAsync(scheduledWorkingPeriods);
        await RockSchoolContext.SaveChangesAsync();
    }

    public async Task DeleteForTeacherAfter(Guid teacherId, DateTime deleteAfter)
    {
        var periodsToDelete = RockSchoolContext.ScheduledWorkingPeriods
            .Where(p => p.TeacherId == teacherId && p.StartDate >= deleteAfter);
        
        RockSchoolContext.ScheduledWorkingPeriods.RemoveRange(periodsToDelete);
        await RockSchoolContext.SaveChangesAsync();
    }
}