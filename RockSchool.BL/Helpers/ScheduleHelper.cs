using RockSchool.BL.Models;
using RockSchool.Data.Enums;

namespace RockSchool.BL.Helpers;

public static class ScheduleHelper
{
    public static List<Attendance> GenerateAttendances(SubscriptionDetails subscriptionDetails, Schedule[] schedules, bool isGroup, Guid? subscriptionId = null)
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
                StudentId = subscriptionDetails.StudentId,
                StartDate = attendanceStartDate,
                EndDate = attendanceStartDate.AddMinutes(lessonMinutes),
                AttendanceType = AttendanceType.Lesson,
                BranchId = subscriptionDetails.BranchId,
                RoomId = availableSlot.RoomId,
                DisciplineId = subscriptionDetails.DisciplineId,
                GroupId = isGroup ? Guid.NewGuid() : null,
            };

            if (subscriptionId != null)
            {
                newAttendance.SubscriptionId = subscriptionId.Value;
            }

            attendances.Add(newAttendance);

            startDate = attendanceStartDate;
            attendancesToAdd--;
        }

        return attendances;
    }

    public static AvailableSlot GetNextAvailableSlot(DateTime startingFrom, Schedule[] orderedSchedules, int lengthInMinutes = 60)
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

    private static Schedule GetNextSchedule(DateTime startingFrom, Schedule[]  orderedSchedules)
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

    private static DateTime GetNextDate(DateTime startingFrom, Schedule schedule)
    {
        var daysUntilNext = (schedule.WeekDay - (int)startingFrom.DayOfWeek + 7) % 7;

        if (daysUntilNext == 0)
        {
            daysUntilNext = 7;
        }

        return startingFrom.AddDays(daysUntilNext);
    }
}