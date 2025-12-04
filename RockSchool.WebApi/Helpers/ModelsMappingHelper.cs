using System.Collections.Generic;
using System.Linq;
using RockSchool.BL.Dtos;
using RockSchool.BL.Models;
using RockSchool.Data.Enums;
using RockSchool.WebApi.Models.Attendances;
using RockSchool.WebApi.Models.Subscriptions;

namespace RockSchool.WebApi.Helpers
{
    public static class ModelsMappingHelper
    {
        // Attendance
        public static AttendanceInfo ToInfo(this Attendance attendance)
        {
            return new AttendanceInfo
            {
                AttendanceId = attendance.AttendanceId,
                SubscriptionId = attendance.SubscriptionId,
                StartDate = attendance.StartDate,
                EndDate = attendance.EndDate,
                Status = (int) attendance.Status,
                StatusReason = attendance.StatusReason,
                RoomId = attendance.RoomId,
                DisciplineId = attendance.DisciplineId,
                Student = attendance.Student,
                Teacher = attendance.Teacher,
                IsTrial = attendance.IsTrial,
                IsCompleted = attendance.IsCompleted,
                GroupId = attendance.GroupId,
                StudentId = attendance.StudentId,
            };
        }

        public static List<AttendanceInfo> ToAttendanceInfos(this IEnumerable<Attendance> attendances)
        {
            return attendances.Select(dto => dto.ToInfo()).ToList();
        }

        public static ParentAttendanceInfo ToParentAttendanceInfo(this Attendance attendance)
        {
            return new ParentAttendanceInfo
            {
                AttendanceId = attendance.AttendanceId,
                SubscriptionId = attendance.SubscriptionId,
                StartDate = attendance.StartDate,
                EndDate = attendance.EndDate,
                Status = (int)attendance.Status,
                StatusReason = attendance.StatusReason,
                RoomId = attendance.RoomId,
                DisciplineId = attendance.DisciplineId,
                Student = attendance.Student,
                Teacher = attendance.Teacher,
                IsTrial = attendance.IsTrial,
                IsCompleted = attendance.IsCompleted,
                GroupId = attendance.GroupId,
            };
        }

        public static List<ParentAttendanceInfo> ToParentAttendanceInfos(this IEnumerable<Attendance> attendances)
        {
            return attendances.Select(dto => dto.ToParentAttendanceInfo()).ToList();
        }

        public static List<Attendance> ToModels(this IEnumerable<AttendanceInfo> attendanceInfos)
        {
            return attendanceInfos.Select(attendanceInfo => attendanceInfo.ToModel()).ToList();
        }

        public static Attendance ToModel(this AttendanceInfo attendanceInfo)
        {
            return new Attendance
            {
                AttendanceId = attendanceInfo.AttendanceId,
                SubscriptionId = attendanceInfo.SubscriptionId,
                StartDate = attendanceInfo.StartDate,
                EndDate = attendanceInfo.EndDate,
                Status = (AttendanceStatus)attendanceInfo.Status,
                StatusReason = attendanceInfo.StatusReason,
                RoomId = attendanceInfo.RoomId,
                DisciplineId = attendanceInfo.DisciplineId,
                //Student = attendanceInfo.Student, DEV
                //Teacher = attendanceInfo.Teacher,
                IsTrial = attendanceInfo.IsTrial,
                GroupId = attendanceInfo.GroupId,
            };
        }

        // Subscription

        public static SubscriptionInfo ToInfo(this Subscription subscription)
        {
            return new SubscriptionInfo
            {
                SubscriptionId = subscription.SubscriptionId,
                StartDate = subscription.StartDate,
                Student = subscription.Student,
                Status = subscription.Status,
                DisciplineId = subscription.DisciplineId,
                TrialStatus = subscription.TrialStatus,
                AttendanceCount = subscription.AttendanceCount,
            };
        }

        public static ParentSubscriptionInfo ToParentSubscriptionInfo(this Subscription subscription)
        {
            return new ParentSubscriptionInfo
            {
                SubscriptionId = subscription.SubscriptionId,
                StartDate = subscription.StartDate,
                Student = subscription.Student,
                Status = subscription.Status,
                DisciplineId = subscription.DisciplineId,
                TrialStatus = subscription.TrialStatus,
                AttendanceCount = subscription.AttendanceCount,
            };
        }

        public static List<ParentSubscriptionInfo> ToParentSubscriptionInfos(this IEnumerable<Subscription> subscriptionDto)
        {
            return subscriptionDto.Select(dto => dto.ToParentSubscriptionInfo()).ToList();
        }

        public static List<SubscriptionInfo> ToSubscriptionInfos(this IEnumerable<Subscription> subscriptionDtos)
        {
            return subscriptionDtos.Select(dto => dto.ToInfo()).ToList();
        }
    }
}
