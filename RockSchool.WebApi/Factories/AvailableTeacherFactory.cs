using System;
using System.Collections.Generic;
using System.Linq;
using RockSchool.BL.Dtos;

namespace RockSchool.WebApi.Factories;

public static class AvailableTeacherFactory
{
    private static object Create(TeacherDto teacher, AttendanceDto[] attendances)
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
                    swp.EndDate
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

    private static object[] CreateMany(TeacherDto[] teachers, Dictionary<Guid, AttendanceDto[]> attendanceMap)
    {
        return teachers.Select(t =>
        {
            var attendances = attendanceMap.TryGetValue(t.TeacherId, out var a)
                ? a
                : Array.Empty<AttendanceDto>();

            return Create(t, attendances);
        }).ToArray();
    }

    public static object CreateResponse(TeacherDto[] teachers, Dictionary<Guid, AttendanceDto[]> attendanceMap)
    {
        return new
        {
            availableTeachers = CreateMany(teachers, attendanceMap)
        };
    }
}