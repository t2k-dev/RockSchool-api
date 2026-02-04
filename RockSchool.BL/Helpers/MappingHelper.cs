using RockSchool.BL.Models;

namespace RockSchool.BL.Helpers
{
    public static class MappingHelper
    {
        /*
        // Attendance
        public static Attendance ToModel(this AttendanceEntity entity)
        {
            return new Attendance
            {
                AttendanceId = entity.AttendanceId,
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
                Room = entity.Room?.ToModel(),
                Comment = entity.Comment,
                IsCompleted = entity.IsCompleted,
                AttendanceType = entity.AttendanceType,
                // Remove Attendees to prevent circular reference
                SubscriptionsAttendances = entity.SubscriptionsAttendances?.ToModel()?.ToList()
            };
        }

        public static Attendance[] ToModel(this IEnumerable<AttendanceEntity> entities)
        {
            return entities.Select(a => a.ToModel())
                .ToArray();
        }

        // Branch
        public static Branch ToDto(this BranchEntity entity)
        {
            if (entity == null) return null;

            return new Branch
            {
                BranchId = entity.BranchId,
                Name = entity.Name,
                Phone = entity.Phone,
                Address = entity.Address,
                Rooms = entity.Rooms?.ToModel()
            };
        }

        // Schedule
        public static Schedule ToModel(this ScheduleEntity entity)
        {
            return new Schedule
            {
                ScheduleId = entity.ScheduleId,
                SubscriptionId = entity.SubscriptionId,
                BandId = entity.BandId,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                WeekDay = entity.WeekDay,
                RoomId = entity.RoomId,
            };
        }

        public static Schedule[] ToModel(this IEnumerable<ScheduleEntity> entities)
        {
            return entities.Select(w => w.ToModel())
                .ToArray();
        }

        // ScheduledWorkingPeriod
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

        // Student
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



        // Subscription
        public static Subscription ToModel(this SubscriptionEntity entity)
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
                TeacherId = entity.TeacherId,
                Teacher = entity.Teacher?.ToDto(),
                BranchId = entity.BranchId,
                Branch = entity.Branch?.ToDto(),
                TrialStatus = entity.TrialStatus,
                TariffId = entity.TariffId,
                Tariff = entity.Tariff?.ToModel(),
                Schedules = entity.Schedules?.ToModel(),
                SubscriptionType = entity.SubscriptionType,
                Price = entity.Price,
                FinalPrice = entity.FinalPrice,
                AmountOutstanding = entity.AmountOutstanding,
                // Remove Attendees to prevent circular reference
                // Attendees = entity.Attendees?.ToModel()?.ToList()
            };
        }

        public static Subscription[] ToModel(this IEnumerable<SubscriptionEntity> entities)
        {
            return entities.Select(w => w.ToModel())
                .ToArray();
        }

        // Room
        public static Room ToModel(this RoomEntity entity)
        {
            if (entity == null) return null;

            return new Room
            {
                RoomId = entity.RoomId,
                BranchId = entity.BranchId,
                Branch = entity.Branch?.ToDto(),
                Name = entity.Name,
                Status = entity.Status,
                IsActive = entity.IsActive,
                SupportsRent = entity.SupportsRent,
                SupportsRehearsal = entity.SupportsRehearsal,
            };
        }

        public static Room[] ToModel(this IEnumerable<RoomEntity> entities)
        {
            return entities.Select(w => w.ToModel())
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
                AllowBands = entity.AllowBands,
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

        // WorkingPeriod
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

        // Tenders
        public static Tender ToModel(this TenderEntity tenderEntity)
        {
            return new Tender
            {
                TenderId = tenderEntity.TenderId,
                Amount = (int)tenderEntity.Amount,
                PaidOn = tenderEntity.PaidOn,
                TenderType = tenderEntity.TenderType,
                SubscriptionId = tenderEntity.SubscriptionId,
            };
        }

        public static Tender[] ToModel(this TenderEntity[] tenderEntities)
        {
            return tenderEntities.Select(t => t.ToModel()).ToArray();
        }

        // Tariff
        public static Tariff ToModel(this TariffEntity entity)
        {
            return new Tariff
            {
                TariffId = entity.TariffId,
                Amount = entity.Amount,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                DisciplineId = entity.DisciplineId,
                //Discipline = entity.Discipline?.ToDto(),
                AttendanceLength = entity.AttendanceLength,
                AttendanceCount = entity.AttendanceCount,
                SubscriptionType = entity.SubscriptionType
            };
        }

        public static Tariff[] ToModel(this IEnumerable<TariffEntity> entities)
        {
            return entities?.Select(t => t.ToModel()).ToArray() ?? [];
        }

        // Band
        public static Band ToModel(this BandEntity entity)
        {
            return new Band
            {
                BandId = entity.BandId,
                Name = entity.Name,
                TeacherId = entity.TeacherId,
                Teacher = entity.Teacher?.ToDto(),
                Status = entity.Status,
                // Remove BandStudents to prevent circular reference when called from BandStudent.ToModel()
                // BandStudents = entity.BandStudents?.Select(bs => bs.ToModel()).ToList()
            };
        }

        public static Band[] ToModel(this IEnumerable<BandEntity> entities)
        {
            return entities?.Select(b => b.ToModel()).ToArray() ?? [];
        }

        // BandStudent
        public static BandStudent ToModel(this BandStudentEntity entity)
        {
            return new BandStudent
            {
                BandStudentId = entity.BandStudentId,
                BandId = entity.BandId,
                Band = entity.Band?.ToModel(),
                StudentId = entity.StudentId,
                Student = entity.Student?.ToDto(),
                BandRoleId = entity.BandRoleId
            };
        }

        public static BandStudent[] ToModel(this IEnumerable<BandStudentEntity> entities)
        {
            return entities?.Select(bs => bs.ToModel()).ToArray() ?? [];
        }

        // Attendees
        public static SubscriptionAttendance ToModel(this AttendeeEntity entity)
        {
            return new SubscriptionAttendance
            {
                SubscriptionAttendanceId = entity.SubscriptionAttendanceId,
                SubscriptionId = entity.SubscriptionId,
                // Remove navigation properties to prevent circular reference
                // Subscription = entity.Subscription?.ToModel(),
                AttendanceId = entity.AttendanceId,
                // Attendance = entity.Attendance?.ToModel(),
                Status = entity.Status,
                StatusReason = entity.StatusReason
            };
        }

        public static SubscriptionAttendance[] ToModel(this IEnumerable<AttendeeEntity> entities)
        {
            return entities?.Select(sa => sa.ToModel()).ToArray() ?? [];
        }
        */
    }
}