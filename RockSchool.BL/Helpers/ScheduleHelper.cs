using RockSchool.BL.Models;
using RockSchool.Data.Enums;

namespace RockSchool.BL.Helpers;

public static class ScheduleHelper
{
    public static List<Attendance> GenerateTemplateAttendances(SubscriptionDetails subscriptionDetails, ScheduleDto[] schedules, bool isGroup)
    {
        var attendances = new List<Attendance>();

        var attendancesToAdd = subscriptionDetails.AttendanceCount;
        var lessonMinutes = subscriptionDetails.AttendanceLength == 1 ? 60 : 90;
        var startDate = subscriptionDetails.StartDate.ToDateTime(TimeOnly.MinValue);
        var orderedSchedules = schedules
            .OrderBy(s => s.WeekDay)
            .ThenBy(s => s.StartTime)
            .ToArray();

        while (attendancesToAdd > 0)
        {
            var availableSlot = GetNextAvailableSlot(startDate, orderedSchedules);
            var attendanceStartDate = availableSlot.StartDate;

            var newAttendance = new Attendance
            {
                Status = AttendanceStatus.New,
                TeacherId = subscriptionDetails.TeacherId,
                StartDate = attendanceStartDate,
                EndDate = attendanceStartDate.AddMinutes(lessonMinutes),
                IsTrial = false,
                BranchId = subscriptionDetails.BranchId,
                RoomId = availableSlot.RoomId,
                DisciplineId = subscriptionDetails.DisciplineId,
                GroupId = isGroup ? Guid.NewGuid() : null,
            };

            attendances.Add(newAttendance);

            startDate = attendanceStartDate;
            attendancesToAdd--;
        }

        return attendances;
    }

    public static AvailableSlot GetNextAvailableSlot(DateTime startingFrom, ScheduleDto[] orderedSchedules)
    {
        const int timeZone = 5; // DEV move somewhere
        var schedule = GetNextSchedule(startingFrom, orderedSchedules);
        var date = GetNextDate(startingFrom, schedule);

        var startDate = new DateTime(date.Year, date.Month, date.Day, schedule.StartTime.Hours - timeZone, schedule.StartTime.Minutes, 0, DateTimeKind.Utc);

        return new AvailableSlot
        {
            StartDate = startDate,
            RoomId = schedule.RoomId,
        };
    }

    private static ScheduleDto GetNextSchedule(DateTime startingFrom, ScheduleDto[]  orderedSchedules)
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

    private static DateTime GetNextDate(DateTime startingFrom, ScheduleDto schedule)
    {
        var daysUntilNext = (schedule.WeekDay - (int)startingFrom.DayOfWeek + 7) % 7;

        if (daysUntilNext == 0)
        {
            daysUntilNext = 7;
        }

        return startingFrom.AddDays(daysUntilNext);
    }
}