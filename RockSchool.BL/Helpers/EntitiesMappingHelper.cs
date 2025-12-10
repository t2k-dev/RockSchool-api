using RockSchool.BL.Models;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockSchool.BL.Helpers
{
    public static class EntitiesMappingHelper
    {
        public static List<WorkingPeriodEntity> ToEntities(this IEnumerable<WorkingPeriod> workingPeriods)
        {
            return workingPeriods.Select(w => w.ToEntity())
                .ToList();
        }

        public static WorkingPeriodEntity ToEntity(this WorkingPeriod workingPeriod)
        {
            return new WorkingPeriodEntity
            {
                StartTime = workingPeriod.StartTime,
                EndTime = workingPeriod.EndTime,
                WeekDay = workingPeriod.WeekDay,
                RoomId = workingPeriod.RoomId,
            };
        }

        public static List<AttendanceEntity> ToEntities(this IEnumerable<Attendance> attendances)
        {
            return attendances.Select(a => a.ToEntity())
                .ToList();
        }

        public static AttendanceEntity ToEntity(this Attendance attendance)
        {
            return new AttendanceEntity
            {
                StudentId = attendance.StudentId,
                TeacherId = attendance.TeacherId,
                BranchId = attendance.BranchId,
                StartDate = attendance.StartDate,
                EndDate = attendance.EndDate,
                Status = attendance.Status,
                RoomId = attendance.RoomId,
                Comment = attendance.Comment,
                SubscriptionId = attendance.SubscriptionId,
                StatusReason = attendance.StatusReason,
                DisciplineId = attendance.DisciplineId,
                GroupId = attendance.GroupId,
                IsTrial = attendance.IsTrial,
                IsCompleted = attendance.IsCompleted,
            };
        }

        public static List<ScheduleEntity> ToEntities(this IEnumerable<Schedule> schedules)
        {
            return schedules.Select(a => a.ToEntity())
                .ToList();
        }

        public static ScheduleEntity ToEntity(this Schedule schedule)
        {
            return new ScheduleEntity
            {
                SubscriptionId = schedule.SubscriptionId,
                WeekDay = schedule.WeekDay,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                RoomId = schedule.RoomId,
            };
        }
    }
}
