using RockSchool.Data.Entities;

namespace RockSchool.BL.Helpers;

public static class ScheduleHelper
{
    public static AvailableSlot GetNextAvailableSlot(DateTime startingFrom, ScheduleEntity[] schedules)
    {
        // TODO: remove ordering from here
        var orderedSchedules = schedules.OrderBy(s => s.WeekDay).ToArray();

        // Get week day for next attendance 

        var schedule = GetCorrespondingSchedule(startingFrom, orderedSchedules);
        var date = GetNextDate(startingFrom, schedule);

        var startDate = new DateTime(date.Year, date.Month, date.Day, schedule.StartTime.Hour, schedule.StartTime.Minute, 0);
        startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
        //var startDate = new DateTimeOffset(year, month, day, schedule.StartTime.Hour, schedule.StartTime.Minute, 0, TimeSpan.Zero);

        return new AvailableSlot
        {
            StartDate = startDate,
            RoomId = schedule.RoomId,
        };
    }

    private static ScheduleEntity GetCorrespondingSchedule(DateTime startingFrom, ScheduleEntity[]  orderedSchedules)
    {
        foreach (var schedule in orderedSchedules)
        {
            if (schedule.WeekDay > (int)startingFrom.DayOfWeek)
            {
                return schedule;
            }
        }

        return orderedSchedules[0];
    }

    private static DateTime GetNextDate(DateTime startingFrom, ScheduleEntity schedule)
    {
        var daysUntilNext = (schedule.WeekDay - (int)startingFrom.DayOfWeek + 7) % 7;

        if (daysUntilNext == 0)
        {
            daysUntilNext = 7;
        }

        return startingFrom.AddDays(daysUntilNext);
    }

    public class AvailableSlot
    {
        public DateTime StartDate { get; set; }
        public int RoomId { get; set; }
    }
}