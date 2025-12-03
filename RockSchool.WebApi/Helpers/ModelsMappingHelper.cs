using System.Collections.Generic;
using System.Linq;
using RockSchool.BL.Dtos;
using RockSchool.Data.Enums;
using RockSchool.WebApi.Models.Attendances;
using RockSchool.WebApi.Models.Subscriptions;

namespace RockSchool.WebApi.Helpers
{
    public static class ModelsMappingHelper
    {
        // Attendance
        public static AttendanceInfo ToInfo(this AttendanceDto attendanceDto)
        {
            return new AttendanceInfo
            {
                AttendanceId = attendanceDto.AttendanceId,
                SubscriptionId = attendanceDto.SubscriptionId,
                StartDate = attendanceDto.StartDate,
                EndDate = attendanceDto.EndDate,
                Status = (int)attendanceDto.Status,
                StatusReason = attendanceDto.StatusReason,
                RoomId = attendanceDto.RoomId,
                DisciplineId = attendanceDto.DisciplineId,
                Student = attendanceDto.Student,
                Teacher = attendanceDto.Teacher,
                IsTrial = attendanceDto.IsTrial
            };
        }

        public static List<AttendanceInfo> ToAttendanceInfos(this IEnumerable<AttendanceDto> attendanceDtos)
        {
            return attendanceDtos.Select(dto => dto.ToInfo()).ToList();
        }

        public static ParentAttendanceInfo ToParentAttendanceInfo(this AttendanceDto attendanceDto)
        {
            return new ParentAttendanceInfo
            {
                AttendanceId = attendanceDto.AttendanceId,
                SubscriptionId = attendanceDto.SubscriptionId,
                StartDate = attendanceDto.StartDate,
                EndDate = attendanceDto.EndDate,
                Status = (int)attendanceDto.Status,
                StatusReason = attendanceDto.StatusReason,
                RoomId = attendanceDto.RoomId,
                DisciplineId = attendanceDto.DisciplineId,
                Student = attendanceDto.Student,
                Teacher = attendanceDto.Teacher,
                IsTrial = attendanceDto.IsTrial,
                GroupId = attendanceDto.GroupId,
            };
        }

        public static List<ParentAttendanceInfo> ToParentAttendanceInfos(this IEnumerable<AttendanceDto> attendanceDtos)
        {
            return attendanceDtos.Select(dto => dto.ToParentAttendanceInfo()).ToList();
        }

        public static List<AttendanceDto> ToDtos(this IEnumerable<AttendanceInfo> attendanceInfos)
        {
            return attendanceInfos.Select(attendanceInfo => attendanceInfo.ToDto()).ToList();
        }

        public static AttendanceDto ToDto(this AttendanceInfo attendanceInfo)
        {
            return new AttendanceDto
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

        public static SubscriptionInfo ToInfo(this SubscriptionDto subscription)
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

        public static ParentSubscriptionInfo ToParentSubscriptionInfo(this SubscriptionDto subscription)
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

        public static List<ParentSubscriptionInfo> ToParentSubscriptionInfos(this IEnumerable<SubscriptionDto> subscriptionDto)
        {
            return subscriptionDto.Select(dto => dto.ToParentSubscriptionInfo()).ToList();
        }

        public static List<SubscriptionInfo> ToSubscriptionInfos(this IEnumerable<SubscriptionDto> subscriptionDtos)
        {
            return subscriptionDtos.Select(dto => dto.ToInfo()).ToList();
        }
    }
}
