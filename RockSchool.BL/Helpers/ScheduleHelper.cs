using RockSchool.BL.Models;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;

namespace RockSchool.BL.Helpers;

public static class ScheduleHelper
{
    public static List<Attendance> GenerateAttendances(SubscriptionDetails subscriptionDetails, ScheduleDto[] schedules, bool isGroup, Guid? subscriptionId = null)
    {
        var attendances = new List<Attendance>();

        var attendancesToAdd = subscriptionDetails.AttendanceCount;
        var lessonMinutes = subscriptionDetails.AttendanceLength;
        var startDate = subscriptionDetails.StartDate.ToDateTime(TimeOnly.MinValue);
        var orderedSchedules = schedules
            .OrderBy(s => s.WeekDay)
            .ThenBy(s => s.StartTime)
            .ToArray();

        while (attendancesToAdd > 0)
        {
            var availableSlot = GetNextAvailableSlot(startDate, orderedSchedules);
            var attendanceStartDate = availableSlot.StartDate;

            var newAttendance = Attendance.Create(
                attendanceStartDate,
                attendanceStartDate.AddMinutes(lessonMinutes),
                availableSlot.RoomId,
                subscriptionDetails.BranchId,
                isGroup ? AttendanceType.GroupLesson : AttendanceType.Lesson,
                subscriptionDetails.DisciplineId,
                subscriptionDetails.TeacherId,
                isGroup ? Guid.NewGuid() : null
                );  

            attendances.Add(newAttendance);

            startDate = attendanceStartDate;
            attendancesToAdd--;
        }

        return attendances;
    }

    public static AvailableSlot GetNextAvailableSlot(DateTime startingFrom, ScheduleDto[] orderedSchedules, int lengthInMinutes = 60)
    {
        const int timeZone = 5; // DEV move somewhere
        var schedule = GetNextSchedule(startingFrom, orderedSchedules);
        var date = GetNextDate(startingFrom, schedule);

        var startDate = new DateTime(date.Year, date.Month, date.Day, schedule.StartTime.Hours - timeZone, schedule.StartTime.Minutes, 0, DateTimeKind.Utc);
        var endDate = startDate.AddMinutes(lengthInMinutes);

        return new AvailableSlot
        {
            StartDate = startDate,
            EndDate = endDate,
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