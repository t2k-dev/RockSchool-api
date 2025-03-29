using RockSchool.BL.Dtos;
using RockSchool.Data.Entities;

namespace RockSchool.BL.Helpers
{
    public static class MappingHelper
    {
        public static WorkingPeriodDto ToDto(this WorkingPeriodEntity entity)
        {
            return new WorkingPeriodDto
            {
                WorkingPeriodId = entity.WorkingPeriodId,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                WeekDay = entity.WeekDay,
            };
        }

        public static WorkingPeriodDto[] ToDto(this IEnumerable<WorkingPeriodEntity> entities)
        {
            return entities.Select(w => w.ToDto())
                .ToArray();
        }

        public static ScheduledWorkingPeriodDto ToDto(this ScheduledWorkingPeriodEntity entity)
        {
            return new ScheduledWorkingPeriodDto
            {
                ScheduledWorkingPeriodId = entity.ScheduledWorkingPeriodId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
            };
        }

        public static ScheduledWorkingPeriodDto[] ToDto(this IEnumerable<ScheduledWorkingPeriodEntity> entities)
        {
            return entities.Select(w => w.ToDto())
                .ToArray();
        }

        public static AttendanceDto ToDto(this AttendanceEntity entity)
        {
            if (entity == null) return null;

            return new AttendanceDto
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
                IsGroup = entity.IsGroup,
                RoomId = entity.RoomId,
                Room = entity.Room,
                Comment = entity.Comment,
            };
        }

        public static StudentDto ToDto(this StudentEntity entity)
        {
            if (entity == null) return null;

            return new StudentDto
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

        public static BranchDto ToDto(this BranchEntity entity)
        {
            if (entity == null) return null;

            return new BranchDto
            {
                BranchId = entity.BranchId,
                Name = entity.Name,
                Phone = entity.Phone,
                Address = entity.Address,
                Rooms = entity.Rooms?.ToDto()
            };
        }

        public static AttendanceDto[] ToDto(this IEnumerable<AttendanceEntity> entities)
        {
            return entities.Select(w => w.ToDto())
                .ToArray();
        }

        public static RoomDto ToDto(this RoomEntity entity)
        {
            if (entity == null) return null;

            return new RoomDto
            {
                RoomId = entity.RoomId,
                BranchId = entity.BranchId,
                Branch = entity.Branch?.ToDto(),
                Name = entity.Name,
                Status = entity.Status,
                IsActive = entity.IsActive
            };
        }

        public static RoomDto[] ToDto(this IEnumerable<RoomEntity> entities)
        {
            return entities.Select(w => w.ToDto())
                .ToArray();
        }

        public static SubscriptionDto ToDto(this SubscriptionEntity entity)
        {
            if (entity == null) return null;

            return new SubscriptionDto
            {
                SubscriptionId = entity.SubscriptionId,
                IsGroup = entity.IsGroup,
                StudentId = entity.StudentId,
                Student = entity.Student?.ToDto(),
                AttendanceCount = entity.AttendanceCount,
                AttendanceLength = entity.AttendanceLength,
                StartDate = entity.StartDate,
                Status = entity.Status,
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline?.ToDto(),
                TransactionId = entity.TransactionId,
                TeacherId = entity.TeacherId,
                Teacher = entity.Teacher?.ToDto(),
                BranchId = entity.BranchId,
                Branch = entity.Branch?.ToDto(),
                TrialStatus = entity.TrialStatus
            };
        }

        public static DisciplineDto ToDto(this DisciplineEntity entity)
        {
            if (entity == null) return null;

            return new DisciplineDto
            {
                DisciplineId = entity.DisciplineId,
                Name = entity.Name,
                IsActive = entity.IsActive,
                Teachers = entity.Teachers.ToDto()
            };
        }

        public static DisciplineDto[] ToDto(this IEnumerable<DisciplineEntity> entities)
        {
            return entities.Select(d => d.ToDto())
                .ToArray();
        }

        public static TeacherDto ToDto(this TeacherEntity entity)
        {
            if (entity == null) return null;

            return new TeacherDto
            {
                TeacherId = entity.TeacherId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                Sex = entity.Sex,
                Phone = entity.Phone,
                User = entity.User,
                BranchId = entity.BranchId,
                Branch = entity.Branch?.ToDto(),
                AgeLimit = entity.AgeLimit,
                AllowGroupLessons = entity.AllowGroupLessons,
                Disciplines = entity.Disciplines.ToDto(),
                WorkingPeriods = entity.WorkingPeriods.ToDto(),
                ScheduledWorkingPeriods = entity.ScheduledWorkingPeriods.ToDto(),
                DisciplineIds = entity.Disciplines?
                    .Select(d => d.DisciplineId)
                    .ToArray()
            };
        }

        public static TeacherDto[] ToDto(this IEnumerable<TeacherEntity> entities)
        {
            return entities.Select(t => t.ToDto()).ToArray();
        }
    }
}