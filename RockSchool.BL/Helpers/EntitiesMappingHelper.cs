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

        public static SubscriptionEntity ToEntity(this Subscription subscription)
        {
            return new SubscriptionEntity
            {
                AttendanceCount = subscription.AttendanceCount,
                AttendancesLeft = subscription.AttendancesLeft,
                AttendanceLength = subscription.AttendanceLength,
                BranchId = subscription.BranchId,
                DisciplineId = subscription.DisciplineId,
                GroupId = subscription.GroupId,
                StartDate = subscription.StartDate,
                Status = subscription.Status,
                StudentId = subscription.StudentId,
                TeacherId = subscription.TeacherId,
                TrialStatus = subscription.TrialStatus,
                StatusReason = subscription.StatusReason,
                SubscriptionType = subscription.SubscriptionType,
                Price = subscription.Price,
                FinalPrice = subscription.FinalPrice,
                AmountOutstanding = subscription.AmountOutstanding,
                TariffId = subscription.TariffId,
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

        public static TenderEntity ToEntity(this Tender payment)
        {
            return new TenderEntity
            {
                TenderId = payment.TenderId,
                Amount = payment.Amount,
                PaidOn = payment.PaidOn,
                TenderType = payment.TenderType,
                SubscriptionId = payment.SubscriptionId,
            };
        }

        public static TariffEntity ToEntity(this Tariff model)
        {
            return new TariffEntity
            {
                TariffId = model.TariffId,
                Amount = model.Amount,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                DisciplineId = model.DisciplineId,
                AttendanceLength = model.AttendanceLength,
                AttendanceCount = model.AttendanceCount,
                SubscriptionType = model.SubscriptionType
            };
        }
    }
}
