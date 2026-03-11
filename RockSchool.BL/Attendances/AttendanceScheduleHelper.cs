using RockSchool.BL.Models;
using RockSchool.BL.Subscriptions;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;

namespace RockSchool.BL.Attendances;

public static class AttendanceScheduleHelper
{
    public static List<Attendance> Generate(
        int attendancesToAdd,
        int lessonMinutes,
        DateOnly startSince,
        int branchId,
        int? disciplineId,
        Guid? teacherId,
        ScheduleSlotDto[] scheduleSlots, 
        AttendanceType attendanceType, 
        Guid? bandId = null,
        Guid? groupId = null)
    {
        var attendances = new List<Attendance>();

        var startDate = startSince.ToDateTime(TimeOnly.MinValue);
        var orderedScheduleSlots = scheduleSlots
            .OrderBy(s => s.WeekDay)
            .ThenBy(s => s.StartTime)
            .ToArray();

        while (attendancesToAdd > 0)
        {
            var availableSlot = GetNextAvailableSlot(startDate, orderedScheduleSlots);
            var attendanceStartDate = availableSlot.StartDate;

            var newAttendance = Attendance.Create(
                attendanceStartDate,
                attendanceStartDate.AddMinutes(lessonMinutes),
                availableSlot.RoomId,
                branchId,
                attendanceType,
                bandId,
                disciplineId,
                teacherId,
                groupId
                );  

            attendances.Add(newAttendance);

            startDate = attendanceStartDate;
            attendancesToAdd--;
        }

        return attendances;
    }

    public static AvailableSlot GetNextAvailableSlot(DateTime startingFrom, ScheduleSlotDto[] orderedSchedules, int lengthInMinutes = 60)
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

    private static ScheduleSlotDto GetNextSchedule(DateTime startingFrom, ScheduleSlotDto[]  orderedSchedules)
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

    private static DateTime GetNextDate(DateTime startingFrom, ScheduleSlotDto schedule)
    {
        var daysUntilNext = (schedule.WeekDay - (int)startingFrom.DayOfWeek + 7) % 7;

        if (daysUntilNext == 0)
        {
            daysUntilNext = 7;
        }

        return startingFrom.AddDays(daysUntilNext);
    }
}