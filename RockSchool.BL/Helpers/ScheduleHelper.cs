using RockSchool.Data.Entities;

namespace RockSchool.BL.Helpers;

public static class ScheduleHelper
{
    public static DateTime GetNextWeekday(DateTime start, int day)
    {
        var daysToAdd = (day - (int)start.DayOfWeek + 7) % 7;
        return start.AddDays(daysToAdd);
    }

    public static DateTime GetNextAttendanceDate(DateTime startingFrom, ScheduleEntity[] orderedSchedules)
    {
        var startingDayOfWeek = (int)startingFrom.DayOfWeek;

        // Get week day for next attendance 
        int? nextAttendanceDay = null;
        foreach (var schedule in orderedSchedules)
        {
            if (schedule.WeekDay > startingDayOfWeek)
            {
                nextAttendanceDay = schedule.WeekDay;
                break;
            }
        }

        nextAttendanceDay ??= orderedSchedules[0].WeekDay;

        // Get the actual date
        var today = DateTime.Today;
        var daysUntilNext = (nextAttendanceDay.Value - (int)today.DayOfWeek + 7) % 7;

        if (daysUntilNext == 0)
        {
            daysUntilNext = 7;
        }

        // Get the target date
        return today.AddDays(daysUntilNext);
    }
}