using RockSchool.Data.Entities;

namespace RockSchool.BL.Helpers;

public static class ScheduleHelper
{
    public static DateTime GetNextWeekday(DateTime start, int day)
    {
        var daysToAdd = (day - (int)start.DayOfWeek + 7) % 7;
        return start.AddDays(daysToAdd);
    }

    public static AvailableSlot GetNextAvailableSlot(DateTime startingFrom, ScheduleEntity[] schedules)
    {
        var orderedSchedules = schedules.OrderBy(s => s.WeekDay).ToArray();
        var startingDayOfWeek = (int)startingFrom.DayOfWeek;

        var roomId = 0;

        // Get week day for next attendance 
        int? nextAttendanceDay = null;
        foreach (var schedule in orderedSchedules)
        {
            if (schedule.WeekDay > startingDayOfWeek)
            {
                nextAttendanceDay = schedule.WeekDay;
                roomId = schedule.RoomId;
                break;
            }
        }

        if (nextAttendanceDay == null)
        {
            nextAttendanceDay = orderedSchedules[0].WeekDay;
            roomId = orderedSchedules[0].RoomId;
        }

        // Get the actual date
        var daysUntilNext = (nextAttendanceDay.Value - (int)startingFrom.DayOfWeek + 7) % 7;

        if (daysUntilNext == 0)
        {
            daysUntilNext = 7;
        }

        return new AvailableSlot
        {
            // Get the target date
            StartDate = startingFrom.AddDays(daysUntilNext),
            RoomId = roomId,
        };
    }

    public class AvailableSlot
    {
        public DateTime StartDate { get; set; }
        public int RoomId { get; set; }
    }
}