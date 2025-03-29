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
    }
}
