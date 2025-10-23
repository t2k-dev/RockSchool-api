using RockSchool.Data.Entities;

namespace RockSchool.BL.Services.ScheduledWorkingPeriodsService
{
    public interface IScheduledWorkingPeriodsService
    {
        Task AddPeriods(Guid teacherId, DateTime startDate, int months, List<WorkingPeriodEntity> workingPeriodEntities);
    }
}
