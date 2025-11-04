using System.Collections.Generic;
using System.Linq;
using RockSchool.BL.Dtos;
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
            };
        }

        public static List<ParentAttendanceInfo> ToParentAttendanceInfos(this IEnumerable<AttendanceDto> attendanceDtos)
        {
            return attendanceDtos.Select(dto => dto.ToParentAttendanceInfo()).ToList();
        }
    }
}
