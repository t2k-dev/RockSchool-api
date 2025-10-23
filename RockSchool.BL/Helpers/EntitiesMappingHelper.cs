using RockSchool.BL.Dtos;
using RockSchool.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockSchool.BL.Helpers
{
    public static class EntitiesMappingHelper
    {
        public static List<WorkingPeriodEntity> ToEntities(this IEnumerable<WorkingPeriodDto> workingPeriods)
        {
            return workingPeriods.Select(w => w.ToEntity())
                .ToList();
        }

        public static WorkingPeriodEntity ToEntity(this WorkingPeriodDto workingPeriod)
        {
            return new WorkingPeriodEntity
            {
                StartTime = workingPeriod.StartTime,
                EndTime = workingPeriod.EndTime,
                WeekDay = workingPeriod.WeekDay,
                RoomId = workingPeriod.RoomId,
            };
        }
    }
}
