using System;
using System.Collections.Generic;
using System.Linq;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Attendances;
using RockSchool.WebApi.Models.Bands;
using RockSchool.WebApi.Models.Subscriptions;
using RockSchool.WebApi.Models.Teachers;

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
                Teacher = attendance.Teacher,
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
                Teacher = attendance.Teacher,
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

        // Subscription
        public static SubscriptionInfo ToInfo(this Subscription subscription)
        {
            return new SubscriptionInfo
            {
                SubscriptionId = subscription.SubscriptionId,
                StartDate = subscription.StartDate,
                Student = subscription.Student,
                Status = (int)subscription.Status,
                DisciplineId = subscription.DisciplineId,
                TrialStatus = subscription.TrialStatus,
                AttendanceCount = subscription.AttendanceCount,
                AttendanceLength = subscription.AttendanceLength,
                AttendancesLeft = subscription.AttendancesLeft,
                Teacher = subscription.Teacher,
                Schedules = subscription.Schedules?.ToInfos(),
                SubscriptionType = (int)subscription.SubscriptionType,
                AmountOutstanding = subscription.AmountOutstanding,
                Price = subscription.Price,
                FinalPrice = subscription.FinalPrice,
            };
        }

        public static ParentSubscriptionInfo ToParentSubscriptionInfo(this Subscription subscription)
        {
            return new ParentSubscriptionInfo
            {
                SubscriptionId = subscription.SubscriptionId,
                StartDate = subscription.StartDate,
                Student = subscription.Student,
                Status = (int)subscription.Status,
                DisciplineId = subscription.DisciplineId,
                TrialStatus = subscription.TrialStatus,
                AttendanceCount = subscription.AttendanceCount,
                AttendancesLeft = subscription.AttendancesLeft,
            };
        }

        public static List<ParentSubscriptionInfo> ToParentSubscriptionInfos(this IEnumerable<Subscription> subscriptionDto)
        {
            return subscriptionDto.Select(dto => dto.ToParentSubscriptionInfo()).ToList();
        }

        public static List<SubscriptionInfo> ToSubscriptionInfos(this IEnumerable<Subscription> subscriptions)
        {
            return subscriptions.Select(model => model.ToInfo()).ToList();
        }

        // Schedule

        public static ScheduleInfo ToInfo(this Schedule schedule)
        {
            var scheduleInfo = new ScheduleInfo
                {
                    ScheduleId = schedule.ScheduleId,
                    SubscriptionId = schedule.SubscriptionId,
                    RoomId = schedule.RoomId,
                    WeekDay = schedule.WeekDay,
                    StartTime = schedule.StartTime.ToString(@"hh\:mm"),
                    EndTime = schedule.EndTime.ToString(@"hh\:mm"),
                };
            return scheduleInfo;
        }

        public static ScheduleInfo[] ToInfos(this IEnumerable<Schedule> schedules)
        {
            return schedules.Select(model => model.ToInfo()).ToArray();
        }

        public static Schedule ToModel(this ScheduleInfo scheduleInfo, Guid subscriptionId)
        {
            throw new NotImplementedException();
            /*
            var schedule = new Schedule
            {
                ScheduleId = scheduleInfo.ScheduleId,
                SubscriptionId = subscriptionId,
                RoomId = scheduleInfo.RoomId,
                WeekDay = scheduleInfo.WeekDay,
                StartTime =  TimeSpan.Parse(scheduleInfo.StartTime),
                EndTime = TimeSpan.Parse(scheduleInfo.EndTime),
            };
            return schedule;*/
        }

        public static List<Schedule> ToModel(this IEnumerable<ScheduleInfo> scheduleInfos, Guid subscriptionId)
        {
            return scheduleInfos.Select(model => model.ToModel(subscriptionId)).ToList();
        }

        // Teacher

        public static TeacherInfo ToInfo(this Teacher teacher)
        {
            throw new NotImplementedException();
            /*
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
                Disciplines = teacher.DisciplineIds,
                TeacherId = teacher.TeacherId,
            };*/
        }

        public static List<TeacherInfo> ToInfos(this IEnumerable<Teacher> items)
        {
            return items.Select(model => model.ToInfo()).ToList();
        }

        // Tender
        public static TenderInfo ToInfo(this Tender tender)
        {

            return new TenderInfo
            {
                TenderId = tender.TenderId,
                Amount = tender.Amount,
                PaidOn = tender.PaidOn,
                TenderType = (int)tender.TenderType,
                SubscriptionId = tender.SubscriptionId,
            };
        }

        public static TenderInfo[] ToInfos(this Tender[] tenders)
        {
            return tenders.Select(t => t.ToInfo()).ToArray();
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
                BandStudents = band.BandStudents?.Select(bs => bs.ToInfo()).ToArray()
            };
        }

        public static BandInfo[] ToInfos(this Band[] bands)
        {
            return bands.Select(b => b.ToInfo()).ToArray();
        }

        // BandStudent
        public static BandStudentInfo ToInfo(this BandStudent bandStudent)
        {
            return new BandStudentInfo
            {
                BandStudentId = bandStudent.BandStudentId,
                BandId = bandStudent.BandId,
                StudentId = bandStudent.StudentId,
                Student = bandStudent.Student,
                BandRoleId = bandStudent.BandRoleId
            };
        }

        public static BandStudentInfo[] ToInfos(this BandStudent[] bandStudents)
        {
            return bandStudents.Select(bs => bs.ToInfo()).ToArray();
        }

        public static BandStudent ToModel(this AddStudentToBandDto dto)
        {
            throw new NotImplementedException();
            /*
            return new BandStudent
            {
                BandId = dto.BandId,
                StudentId = dto.StudentId,
                BandRoleId = dto.BandRoleId
            };*/
        }
    }
}
