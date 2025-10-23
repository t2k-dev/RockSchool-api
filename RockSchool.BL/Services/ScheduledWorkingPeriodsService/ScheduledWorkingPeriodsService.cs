using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.ScheduledWorkingPeriodsService
{
    public class ScheduledWorkingPeriodsService : IScheduledWorkingPeriodsService
    {
        private readonly ScheduledWorkingPeriodsRepository _scheduledWorkingPeriodsRepository;

        public ScheduledWorkingPeriodsService(ScheduledWorkingPeriodsRepository scheduledWorkingPeriodsRepository)
        {
            _scheduledWorkingPeriodsRepository = scheduledWorkingPeriodsRepository;
        }

        public async Task AddPeriods(Guid teacherId, DateTime startDate, int months, List<WorkingPeriodEntity> workingPeriodEntities)
        {
            var scheduledWorkingPeriodEntities = BuildScheduledWorkingPeriods(teacherId, startDate, months, workingPeriodEntities);
            await _scheduledWorkingPeriodsRepository.AddRangeAsync(scheduledWorkingPeriodEntities);
        }

        private List<ScheduledWorkingPeriodEntity> BuildScheduledWorkingPeriods(Guid teacherId, DateTime startDate, int months, List<WorkingPeriodEntity> workingPeriodEntities)
        {
            var scheduledWorkingPeriodEntities = new List<ScheduledWorkingPeriodEntity>();
            var endDate = startDate.AddMonths(months);

            for (var currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
            {
                foreach (var workingPeriodEntity in workingPeriodEntities)
                {
                    if ((int)currentDate.DayOfWeek == workingPeriodEntity.WeekDay)
                    {
                        var localStart = DateTime.SpecifyKind(currentDate.Add(workingPeriodEntity.StartTime), DateTimeKind.Local);
                        var localEnd = DateTime.SpecifyKind(currentDate.Add(workingPeriodEntity.EndTime), DateTimeKind.Local);

                        var periodStart = localStart.ToUniversalTime();
                        var periodEnd = localEnd.ToUniversalTime();

                        scheduledWorkingPeriodEntities.Add(new ScheduledWorkingPeriodEntity
                        {
                            WorkingPeriodId = workingPeriodEntity.WorkingPeriodId,
                            TeacherId = teacherId,
                            StartDate = periodStart,
                            EndDate = periodEnd,
                            RoomId = workingPeriodEntity.RoomId,
                        });
                    }
                }
            }

            return scheduledWorkingPeriodEntities;
        }
    }
}
