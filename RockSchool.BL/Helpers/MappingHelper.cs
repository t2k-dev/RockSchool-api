using RockSchool.BL.Models;
using RockSchool.Data.Entities;
using RockSchool.Data.Enums;

namespace RockSchool.BL.Helpers
{
    public static class MappingHelper
    {
        public static WorkingPeriod ToDto(this WorkingPeriodEntity entity)
        {
            return new WorkingPeriod
            {
                WorkingPeriodId = entity.WorkingPeriodId,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                WeekDay = entity.WeekDay,
                RoomId = entity.RoomId,
            };
        }

        public static WorkingPeriod[] ToDto(this IEnumerable<WorkingPeriodEntity> entities)
        {
            return entities.Select(w => w.ToDto())
                .ToArray();
        }

        public static Schedule ToDto(this ScheduleEntity entity)
        {
            return new Schedule
            {
                ScheduleId = entity.ScheduleId,
                SubscriptionId = entity.SubscriptionId.Value, // DEV
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                WeekDay = entity.WeekDay,
                RoomId = entity.RoomId,
            };
        }

        public static Schedule[] ToDto(this IEnumerable<ScheduleEntity> entities)
        {
            return entities.Select(w => w.ToDto())
                .ToArray();
        }

        public static ScheduledWorkingPeriod ToDto(this ScheduledWorkingPeriodEntity entity)
        {
            return new ScheduledWorkingPeriod
            {
                ScheduledWorkingPeriodId = entity.ScheduledWorkingPeriodId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                RoomId = entity.RoomId,
            };
        }

        public static ScheduledWorkingPeriod[] ToDto(this IEnumerable<ScheduledWorkingPeriodEntity> entities)
        {
            return entities.Select(w => w.ToDto())
                .ToArray();
        }

        public static Attendance ToModel(this AttendanceEntity entity)
        {
            return new Attendance
            {
                AttendanceId = entity.AttendanceId,
                StudentId = entity.StudentId,
                Student = entity.Student?.ToDto(),
                SubscriptionId = entity.SubscriptionId,
                Subscription = entity.Subscription?.ToDto(),
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline?.ToDto(),
                TeacherId = entity.TeacherId,
                Teacher = entity.Teacher?.ToDto(),
                Status = entity.Status,
                StatusReason = entity.StatusReason,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                GroupId = entity.GroupId,
                RoomId = entity.RoomId,
                BranchId = entity.BranchId,
                Room = entity.Room,
                Comment = entity.Comment,
                IsTrial = entity.IsTrial,
                IsCompleted = entity.IsCompleted
            };
        }

        public static Student ToDto(this StudentEntity entity)
        {
            if (entity == null) return null;

            return new Student
            {
                StudentId = entity.StudentId,
                User = entity.User,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Sex = entity.Sex,
                BirthDate = entity.BirthDate,
                Phone = entity.Phone,
                Level = entity.Level,
                BranchId = entity.Branch?.BranchId,
                Branch = entity.Branch?.ToDto()
            };
        }

        public static Branch ToDto(this BranchEntity entity)
        {
            if (entity == null) return null;

            return new Branch
            {
                BranchId = entity.BranchId,
                Name = entity.Name,
                Phone = entity.Phone,
                Address = entity.Address,
                Rooms = entity.Rooms?.ToDto()
            };
        }

        public static Attendance[] ToDto(this IEnumerable<AttendanceEntity> entities)
        {
            return entities.Select(w => w.ToModel())
                .ToArray();
        }

        public static Room ToDto(this RoomEntity entity)
        {
            if (entity == null) return null;

            return new Room
            {
                RoomId = entity.RoomId,
                BranchId = entity.BranchId,
                Branch = entity.Branch?.ToDto(),
                Name = entity.Name,
                Status = entity.Status,
                IsActive = entity.IsActive
            };
        }

        public static Room[] ToDto(this IEnumerable<RoomEntity> entities)
        {
            return entities.Select(w => w.ToDto())
                .ToArray();
        }

        public static Subscription ToDto(this SubscriptionEntity entity)
        {
            if (entity == null) return null;

            return new Subscription
            {
                SubscriptionId = entity.SubscriptionId,
                GroupId = entity.GroupId,
                StudentId = entity.StudentId,
                Student = entity.Student?.ToDto(),
                AttendanceCount = entity.AttendanceCount,
                AttendancesLeft = entity.AttendancesLeft,
                AttendanceLength = entity.AttendanceLength,
                StartDate = entity.StartDate,
                Status = entity.Status,
                StatusReason = entity.StatusReason,
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline?.ToDto(),
                PaymentId = entity.PaymentId,
                TeacherId = entity.TeacherId,
                Teacher = entity.Teacher?.ToDto(),
                BranchId = entity.BranchId,
                Branch = entity.Branch?.ToDto(),
                TrialStatus = (TrialStatus?)entity.TrialStatus
            };
        }

        public static Subscription[] ToDto(this IEnumerable<SubscriptionEntity> entities)
        {
            return entities.Select(w => w.ToDto())
                .ToArray();
        }

        public static Discipline ToDto(this DisciplineEntity entity)
        {
            if (entity == null) return null;

            return new Discipline
            {
                DisciplineId = entity.DisciplineId,
                Name = entity.Name,
                IsActive = entity.IsActive,
                Teachers = entity.Teachers.ToDto()
            };
        }

        public static Discipline[] ToDto(this IEnumerable<DisciplineEntity> entities)
        {
            return entities.Select(d => d.ToDto())
                .ToArray();
        }

        public static Teacher ToDto(this TeacherEntity entity)
        {
            if (entity == null) return null;

            return new Teacher
            {
                TeacherId = entity.TeacherId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                Sex = entity.Sex,
                Phone = entity.Phone,
                User = entity.User,
                BranchId = entity.BranchId,
                Branch = entity.Branch == null
                    ? null
                    : new Branch
                    {
                        BranchId = entity.Branch.BranchId,
                        Name = entity.Branch.Name,
                        Phone = entity.Branch.Phone,
                        Address = entity.Branch.Address,
                        Rooms = entity.Branch.Rooms?.Select(r => new Room
                        {
                            RoomId = r.RoomId,
                            BranchId = r.BranchId,
                            Name = r.Name,
                            Status = r.Status,
                            IsActive = r.IsActive,
                            Branch = null
                        }).ToList()
                    },
                AgeLimit = entity.AgeLimit,
                AllowGroupLessons = entity.AllowGroupLessons,
                IsActive = entity.IsActive,
                Disciplines = entity.Disciplines?.Select(d => new Discipline
                {
                    DisciplineId = d.DisciplineId,
                    Name = d.Name,
                    IsActive = d.IsActive,
                    Teachers = null
                }).ToList(),

                DisciplineIds = entity.Disciplines?.Select(d => d.DisciplineId).ToArray(),

                WorkingPeriods = entity.WorkingPeriods?.Select(wp => new WorkingPeriod
                {
                    WorkingPeriodId = wp.WorkingPeriodId,
                    TeacherId = wp.TeacherId,
                    StartTime = wp.StartTime,
                    EndTime = wp.EndTime,
                    WeekDay = wp.WeekDay,
                    RoomId = wp.RoomId,
                }).ToList(),

                ScheduledWorkingPeriods = entity.ScheduledWorkingPeriods?.Select(swp => new ScheduledWorkingPeriod
                {
                    ScheduledWorkingPeriodId = swp.ScheduledWorkingPeriodId,
                    TeacherId = swp.TeacherId,
                    StartDate = swp.StartDate,
                    EndDate = swp.EndDate,
                    RoomId = swp.RoomId,
                }).ToList()
            };
        }

        public static Teacher[] ToDto(this IEnumerable<TeacherEntity> entities)
        {
            return entities.Select(t => t.ToDto()).ToArray();
        }
    }
}