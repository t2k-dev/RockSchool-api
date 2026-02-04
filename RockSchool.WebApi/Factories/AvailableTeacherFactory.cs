using System;
using System.Collections.Generic;
using System.Linq;
using RockSchool.BL.Models;
using RockSchool.Domain.Entities;

namespace RockSchool.WebApi.Factories;

public static class AvailableTeacherFactory
{
    private static object Create(Teacher teacher, Attendance[] attendances)
    {
        return new
        {
            teacher.TeacherId,
            teacher.FirstName,
            teacher.LastName,
            WorkLoad = Random.Shared.Next(1, 100),

            ScheduledWorkingPeriods = teacher.ScheduledWorkingPeriods?
                .Select(swp => new
                {
                    swp.ScheduledWorkingPeriodId,
                    swp.StartDate,
                    swp.EndDate,
                    swp.RoomId,
                })
                .ToArray() ?? Array.Empty<object>(),

            Attendancies = attendances
                .Select(a => new
                {
                    a.AttendanceId,
                    a.StartDate,
                    a.EndDate,
                    a.Status
                })
                .ToArray()
        };
    }

    private static object[] CreateMany(Teacher[] teachers, Dictionary<Guid, Attendance[]> attendanceMap)
    {
        return teachers.Select(t =>
        {
            var attendances = attendanceMap.TryGetValue(t.TeacherId, out var a)
                ? a
                : Array.Empty<Attendance>();

            return Create(t, attendances);
        }).ToArray();
    }

    public static object CreateResponse(Teacher[] teachers, Dictionary<Guid, Attendance[]> attendanceMap)
    {
        return new
        {
            availableTeachers = CreateMany(teachers, attendanceMap)
        };
    }
}