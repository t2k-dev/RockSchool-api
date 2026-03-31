using Azure.Core;
using RockSchool.BL.Models;
using RockSchool.BL.Teachers;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;
using RockSchool.Domain.Students;
using RockSchool.Domain.Teachers;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Attendances;
using RockSchool.WebApi.Models.Bands;
using RockSchool.WebApi.Models.Students;
using RockSchool.WebApi.Models.Subscriptions;
using RockSchool.WebApi.Models.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;

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
                StartDate = attendance.StartDate,
                EndDate = attendance.EndDate,
                Status = (int) attendance.Status,
                StatusReason = attendance.StatusReason,
                RoomId = attendance.RoomId,
                DisciplineId = attendance.DisciplineId,
                //Teacher = attendance.Teacher,
                IsCompleted = attendance.IsCompleted,
                GroupId = attendance.GroupId,
                AttendanceType = (int)attendance.AttendanceType,
            };
        }

        public static AttendanceInfo[] ToInfos(this IEnumerable<Attendance> attendances)
        {
            return attendances.Select(dto => dto.ToInfo()).ToArray();
        }

        public static ParentAttendanceInfo ToParentAttendanceInfo(this Attendance attendance)
        {
            return new ParentAttendanceInfo
            {
                AttendanceId = attendance.AttendanceId,
                StartDate = attendance.StartDate,
                EndDate = attendance.EndDate,
                Status = (int)attendance.Status,
                StatusReason = attendance.StatusReason,
                RoomId = attendance.RoomId,
                DisciplineId = attendance.DisciplineId,
                //Teacher = attendance.Teacher,
                AttendanceType = (int)attendance.AttendanceType,
                IsCompleted = attendance.IsCompleted,
                GroupId = attendance.GroupId,
                Comment = attendance.Comment,
            };
        }

        public static List<ParentAttendanceInfo> ToParentAttendanceInfos(this IEnumerable<Attendance> attendances)
        {
            return attendances.Select(model => model.ToParentAttendanceInfo()).ToList();
        }

        public static List<Attendance> ToModels(this IEnumerable<AttendanceInfo> attendanceInfos)
        {
            return attendanceInfos.Select(attendanceInfo => attendanceInfo.ToModel()).ToList();
        }

        public static Attendance ToModel(this AttendanceInfo attendanceInfo)
        {
            throw new NotImplementedException();
            /*
            return new Attendance
            {
                AttendanceId = attendanceInfo.AttendanceId,
                StartDate = attendanceInfo.StartDate,
                EndDate = attendanceInfo.EndDate,
                Status = (AttendanceStatus)attendanceInfo.Status,
                StatusReason = attendanceInfo.StatusReason,
                RoomId = attendanceInfo.RoomId,
                DisciplineId = attendanceInfo.DisciplineId,
                //Student = attendanceInfo.Student, DEV
                //Teacher = attendanceInfo.Teacher,
                AttendanceType = (AttendanceType)attendanceInfo.AttendanceType,
                GroupId = attendanceInfo.GroupId,
            };*/
        }

        public static StudentSubscriptionInfo ToStudentInfo(this Subscription subscription)
        {
            return new StudentSubscriptionInfo
            {
                SubscriptionId = subscription.SubscriptionId,
                StartDate = subscription.StartDate,
                Status = (int)subscription.Status,
                StatusReason = subscription.StatusReason,
                DisciplineId = subscription.DisciplineId,
                TrialDecision = subscription.TrialDecision,
                TeacherId = subscription.TeacherId,
                TeacherFullName = subscription.Teacher != null ? $"{subscription.Teacher.FirstName} {subscription.Teacher.LastName}" : null,
                AttendanceCount = subscription.AttendanceCount,
                AttendanceLength = subscription.AttendanceLength,
                AttendancesLeft = subscription.AttendancesLeft,
                SubscriptionType = (int)subscription.SubscriptionType,
                AmountOutstanding = subscription.AmountOutstanding,
                Price = subscription.Price,
                FinalPrice = subscription.FinalPrice,
            };
        }

        public static List<StudentSubscriptionInfo> ToStudentInfos(this IEnumerable<Subscription> subscriptions)
        {
            return subscriptions.Select(model => model.ToStudentInfo()).ToList();
        }


        public static TeacherSubscriptionInfo ToTeacherInfo(this Subscription subscription)
        {
            return new TeacherSubscriptionInfo
            {
                SubscriptionId = subscription.SubscriptionId,
                StartDate = subscription.StartDate,
                Status = (int)subscription.Status,
                StatusReason = subscription.StatusReason,
                DisciplineId = subscription.DisciplineId,
                TrialDecision = subscription.TrialDecision,
                StudentId = subscription.StudentId,
                StudentFullName = subscription.Student != null ? $"{subscription.Student.FirstName} {subscription.Student.LastName}" : null,
                AttendanceCount = subscription.AttendanceCount,
                AttendanceLength = subscription.AttendanceLength,
                AttendancesLeft = subscription.AttendancesLeft,
                SubscriptionType = (int)subscription.SubscriptionType,
                AmountOutstanding = subscription.AmountOutstanding,
                Price = subscription.Price,
                FinalPrice = subscription.FinalPrice,
            };
        }

        public static List<TeacherSubscriptionInfo> ToTeacherInfos(this IEnumerable<Subscription> subscriptions)
        {
            return subscriptions.Select(model => model.ToTeacherInfo()).ToList();
        }

        // Schedule

        public static ScheduleSlotInfo ToInfo(this ScheduleSlot slot)
        {
            var scheduleInfo = new ScheduleSlotInfo
                {
                    ScheduleId = slot.ScheduleId,
                    WeekDay = slot.WeekDay,
                    StartTime = slot.StartTime.ToString(@"hh\:mm"),
                    EndTime = slot.EndTime.ToString(@"hh\:mm"),
                    RoomId = slot.RoomId,
                };
            return scheduleInfo;
        }

        public static ScheduleSlotInfo[] ToInfos(this IEnumerable<ScheduleSlot> slots)
        {
            return slots.Select(slot => slot.ToInfo()).ToArray();
        }

        public static ScheduleSlot ToModel(this ScheduleSlotInfo scheduleInfo, Guid scheduleId)
        {
            return ScheduleSlot.Create(
                scheduleId,
                scheduleInfo.RoomId,
                scheduleInfo.WeekDay,
                TimeSpan.Parse(scheduleInfo.StartTime),
                TimeSpan.Parse(scheduleInfo.EndTime)
            );
        }

        public static List<ScheduleSlot> ToModel(this IEnumerable<ScheduleSlotInfo> scheduleInfos, Guid scheduleId)
        {
            return scheduleInfos.Select(model => model.ToModel(scheduleId)).ToList();
        }

        public static ScheduleSlotDto ToDto(this ScheduleSlotInfo scheduleInfo)
        {
            return new ScheduleSlotDto
            {
                RoomId = scheduleInfo.RoomId,
                WeekDay = scheduleInfo.WeekDay,
                StartTime = TimeSpan.Parse(scheduleInfo.StartTime),
                EndTime = TimeSpan.Parse(scheduleInfo.EndTime),
            };
        }

        public static ScheduleSlotDto[] ToDto(this IEnumerable<ScheduleSlotInfo> scheduleInfos)
        {
            return scheduleInfos.Select(model => model.ToDto()).ToArray();
        }

        // Teacher

        public static TeacherInfo ToInfo(this Teacher teacher)
        {
            return new TeacherInfo
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                AgeLimit = teacher.AgeLimit,
                BirthDate = teacher.BirthDate,
                IsActive = teacher.IsActive,
                AllowGroupLessons = teacher.AllowGroupLessons,
                AllowBands = teacher.AllowBands,
                BranchId = teacher.BranchId,
                Phone = teacher.Phone,
                ScheduledWorkingPeriods = teacher.ScheduledWorkingPeriods?.ToArray(),
                WorkingPeriods = teacher.WorkingPeriods?.ToArray(),
                Sex = teacher.Sex,
                Disciplines = teacher.Disciplines.Select(d=>d.DisciplineId).ToArray(),
                TeacherId = teacher.TeacherId,
            };
        }

        public static List<TeacherInfo> ToInfos(this IEnumerable<Teacher> items)
        {
            return items.Select(model => model.ToInfo()).ToList();
        }

        // Payment
        public static PaymentInfo ToInfo(this Payment payment)
        {

            return new PaymentInfo
            {
                PaymentId = payment.PaymentId,
                Amount = payment.Amount,
                PaidOn = payment.PaidOn,
                PaymentType = (int)payment.PaymentType,
                SubscriptionId = payment.SubscriptionId,
            };
        }

        public static PaymentInfo[] ToInfos(this Payment[] payments)
        {
            return payments.Select(t => t.ToInfo()).ToArray();
        }

        // Band
        public static BandInfo ToInfo(this Band band)
        {
            return new BandInfo
            {
                BandId = band.BandId,
                Name = band.Name,
                TeacherId = band.TeacherId,
                Teacher = band.Teacher,
                Status = band.Status,
                IsActive = band.IsActive,
                BandMembers = band.BandMembers?.Select(bm => bm.ToInfo()).ToArray()
            };
        }

        public static BandInfo[] ToInfos(this Band[] bands)
        {
            return bands.Select(b => b.ToInfo()).ToArray();
        }

        // BandMember
        public static BandMemberInfo ToInfo(this BandMember bandMember)
        {
            return new BandMemberInfo
            {
                BandMemberId = bandMember.BandMemberId,
                BandId = bandMember.BandId,
                StudentId = bandMember.StudentId,
                Student = bandMember.Student,
                BandRoleId = bandMember.BandRoleId
            };
        }

        public static BandMemberInfo[] ToInfos(this BandMember[] bandMembers)
        {
            return bandMembers.Select(bm => bm.ToInfo()).ToArray();
        }

        // WorkingPeriod
        public static WorkingPeriodDto[] ToDto(this WorkingPeriodInfo[] workingPeriodInfos)
        {
            if (workingPeriodInfos == null || workingPeriodInfos.Length == 0)
                return Array.Empty<WorkingPeriodDto>();

            return workingPeriodInfos.Select(wp => new WorkingPeriodDto
            {
                WorkingPeriodId = wp.WorkingPeriodId,
                TeacherId = wp.TeacherId,
                WeekDay = wp.WeekDay,
                StartTime = wp.StartTime,
                EndTime = wp.EndTime,
                RoomId = wp.RoomId
            }).ToArray();
        }
    }
}
