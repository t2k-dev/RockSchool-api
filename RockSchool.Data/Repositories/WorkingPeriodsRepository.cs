using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Repositories
{
    public class WorkingPeriodsRepository : BaseRepository
    {
        public WorkingPeriodsRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
        {
        }

        public async Task<WorkingPeriod[]?> GetWorkingPeriods(Guid teacherId)
        {
            return await RockSchoolContext.WorkingPeriods.Where(wp => wp.TeacherId == teacherId).ToArrayAsync();
        }
    }
}
