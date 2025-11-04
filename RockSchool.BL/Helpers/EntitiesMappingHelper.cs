using RockSchool.BL.Dtos;
using RockSchool.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockSchool.BL.Helpers
{
    public static class EntitiesMappingHelper
    {
        public static List<WorkingPeriodEntity> ToEntities(this IEnumerable<WorkingPeriodDto> workingPeriods)
        {
            return workingPeriods.Select(w => w.ToEntity())
                .ToList();
        }

        public static WorkingPeriodEntity ToEntity(this WorkingPeriodDto workingPeriod)
        {
            return new WorkingPeriodEntity
            {
                StartTime = workingPeriod.StartTime,
                EndTime = workingPeriod.EndTime,
                WeekDay = workingPeriod.WeekDay,
                RoomId = workingPeriod.RoomId,
            };
        }

        public static List<AttendanceEntity> ToEntities(this IEnumerable<AttendanceDto> attendances)
        {
            return attendances.Select(a => a.ToEntity())
                .ToList();
        }

        public static AttendanceEntity ToEntity(this AttendanceDto attendance)
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
            };
        }


    }
}
