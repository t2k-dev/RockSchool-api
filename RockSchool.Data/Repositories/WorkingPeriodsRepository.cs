using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.Data.Repositories
{
    public class WorkingPeriodsRepository(RockSchoolContext rockSchoolContext)
        : BaseRepository(rockSchoolContext), IWorkingPeriodsRepository
    {
        public async Task<WorkingPeriod[]?> GetWorkingPeriods(Guid teacherId)
        {
            return await RockSchoolContext.WorkingPeriods.Where(wp => wp.TeacherId == teacherId).ToArrayAsync();
        }

        public async Task DeleteWorkingPeriodsByTeacherId(Guid teacherId)
        {
            var periods = await GetWorkingPeriods(teacherId);
            if (periods != null && periods.Length > 0)
            {
                RockSchoolContext.WorkingPeriods.RemoveRange(periods);
            }
        }

        public void DeleteRange(WorkingPeriod[] periods)
        {
            RockSchoolContext.WorkingPeriods.RemoveRange(periods);
        }

        public async Task AddRangeAsync(WorkingPeriod[] periods)
        {
            await RockSchoolContext.WorkingPeriods.AddRangeAsync(periods);
        }
    }
}
