using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Helpers
{
    public static class ScheduleHelper
    {
        public static DateTime GetNextWeekday(DateTime start, int day)
        {
            int daysToAdd = (day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }
    }
}
