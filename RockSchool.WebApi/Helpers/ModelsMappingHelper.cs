using System.Collections.Generic;
using System.Linq;
using RockSchool.BL.Dtos;
using RockSchool.Data.Enums;
using RockSchool.WebApi.Models.Attendances;

namespace RockSchool.WebApi.Helpers
{
    public static class ModelsMappingHelper
    {
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
    }
}
