namespace RockSchool.BL.Helpers;

public static class ScheduleHelper
{
    public static DateTime GetNextWeekday(DateTime start, int day)
    {
        var daysToAdd = (day - (int)start.DayOfWeek + 7) % 7;
        return start.AddDays(daysToAdd);
    }
}