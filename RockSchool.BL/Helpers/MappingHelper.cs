using RockSchool.BL.Dtos;
using RockSchool.Data.Entities;

namespace RockSchool.BL.Helpers
{
    public static class MappingHelper
    {
        public static WorkingPeriodDto ToDto(this WorkingPeriodEntity entity)
        {
            return new WorkingPeriodDto
            {
                WorkingPeriodId = entity.WorkingPeriodId,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                WeekDay = entity.WeekDay,
            };
        }

        public static WorkingPeriodDto[] ToDto(this IEnumerable<WorkingPeriodEntity> entities)
        {
            return entities.Select(w => w.ToDto())
                .ToArray();
        }

        public static ScheduledWorkingPeriodDto ToDto(this ScheduledWorkingPeriodEntity entity)
        {
            return new ScheduledWorkingPeriodDto
            {
                ScheduledWorkingPeriodId = entity.ScheduledWorkingPeriodId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
            };
        }

        public static ScheduledWorkingPeriodDto[] ToDto(this IEnumerable<ScheduledWorkingPeriodEntity> entities)
        {
            return entities.Select(w => w.ToDto())
                .ToArray();
        }
    }
}
