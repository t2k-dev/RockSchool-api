using RockSchool.BL.Dtos;
using RockSchool.BL.Helpers;
using RockSchool.Data.Entities;
using RockSchool.Data.Enums;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.AttendanceService;

public class AttendanceService : IAttendanceService
{
    private readonly AttendanceRepository _attendanceRepository;
    private readonly ScheduleRepository _scheduleRepository;

    public AttendanceService(AttendanceRepository attendanceRepository, ScheduleRepository scheduleRepository)
    {
        _attendanceRepository = attendanceRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task<AttendanceDto[]> GetAllAttendancesAsync()
    {
        var attendances = await _attendanceRepository.GetAllAsync();

        var attendancesDto = attendances.Select(a => new AttendanceDto
        {
            AttendanceId = a.AttendanceId,
            StudentId = a.StudentId,
            Student = a.Student.ToDto(),
            TeacherId = a.TeacherId,
            Teacher = a.Teacher.ToDto(),
            StartDate = a.StartDate,
            Status = a.Status,
            RoomId = a.RoomId,
            Room = a.Room,
            EndDate = a.EndDate,
            Comment = a.Comment,
            DisciplineId = a.DisciplineId,
        }).ToArray();

        return attendancesDto;
    }

    public async Task<AttendanceDto?> GetAttendanceAsync(Guid attendanceId)
    {
        var attendanceEntity = await _attendanceRepository.GetAsync(attendanceId);
        return attendanceEntity?.ToDto();
    }

    public async Task<AttendanceDto[]> GetByBranchIdAsync(int branchId)
    {
        var attendances = await _attendanceRepository.GetByBranchIdAsync(branchId);
        return attendances.ToDto();
    }

    public async Task AddAttendancesToStudentAsync(SubscriptionDto subscription)
    {
        var schedules = await _scheduleRepository.GetAllBySubscriptionIdAsync(subscription.SubscriptionId);
        var startDate = subscription.StartDate;
        var attendancesToAdd = subscription.AttendanceCount;

        while (attendancesToAdd > 0)
        {
            var availableSlot = ScheduleHelper.GetNextAvailableSlot(startDate, schedules);
            var attendanceBeginDate = availableSlot.StartDate;

            var newAttendance = new AttendanceDto
            {
                Status = AttendanceStatus.New,
                StudentId = subscription.StudentId,
                TeacherId = subscription.TeacherId,
                SubscriptionId = subscription.SubscriptionId,
                StartDate = attendanceBeginDate,
                EndDate = attendanceBeginDate.AddMinutes(subscription.AttendanceLength == 1 ? 60 : 90),
                IsTrial = false,
                BranchId = subscription.BranchId,
                RoomId = availableSlot.RoomId,
                DisciplineId = subscription.DisciplineId,
            };

            await AddAttendanceAsync(newAttendance);

            attendancesToAdd--;

            startDate = attendanceBeginDate;
        }
    }

    public async Task<Guid> AddAttendanceAsync(AttendanceDto attendanceDto)
    {
        var attendanceEntity = new AttendanceEntity
        {
            StudentId = attendanceDto.StudentId,
            TeacherId = attendanceDto.TeacherId,
            BranchId = attendanceDto.BranchId,
            StartDate = attendanceDto.StartDate,
            EndDate = attendanceDto.EndDate,
            Status = attendanceDto.Status,
            RoomId = attendanceDto.RoomId,
            Comment = attendanceDto.Comment,
            SubscriptionId = attendanceDto.SubscriptionId,
            StatusReason = attendanceDto.StatusReason,
            DisciplineId = attendanceDto.DisciplineId,
            IsGroup = attendanceDto.IsGroup,
            IsTrial = attendanceDto.IsTrial,
        };
    
        var attendanceId = await _attendanceRepository.AddAsync(attendanceEntity);
        return attendanceId;
    }

    public async Task<AttendanceDto[]?> GetAttendancesByTeacherIdForPeriodOfTime(Guid teacherId, DateTime startDate, DateTime endDate)
    {
        var attendanceEntities = await _attendanceRepository.GetAttendancesByTeacherIdForPeriodOfTimeAsync(
            teacherId,
            DateTime.UtcNow.AddMonths(-1),
            DateTime.UtcNow.AddMonths(1)
        );

        return attendanceEntities?.ToDto();
    }

    public async Task<AttendanceDto[]?> GetAttendancesByStudentId(Guid studentId)
    {
        var attendanceEntities = await _attendanceRepository.GetAttendancesByStudentIdAsync(studentId);

        return attendanceEntities?.ToDto();
    }

    public async Task UpdateAttendanceAsync(AttendanceDto attendanceDto)
    {
        var existingEntity = await _attendanceRepository.GetAsync(attendanceDto.AttendanceId);

        if (existingEntity == null)
            throw new NullReferenceException("StudentEntity not found.");

        ModifyAttendanceAttributes(attendanceDto, existingEntity);

        await _attendanceRepository.UpdateAsync(existingEntity);
    }

    public async Task UpdateStatusAsync(Guid attendanceId, int status)
    {
        // TODO: discuss implementation
        var attendanceEntity = await _attendanceRepository.GetAsync(attendanceId);

        if (attendanceEntity == null)
            throw new NullReferenceException("StudentEntity not found.");

        attendanceEntity.Status = (AttendanceStatus)status;

        await _attendanceRepository.UpdateAsync(attendanceEntity);
    }

    private static void ModifyAttendanceAttributes(AttendanceDto updatedAttendance,
        AttendanceEntity existingEntity)
    {

        if (updatedAttendance.Status != default)
            existingEntity.Status = updatedAttendance.Status;
        
        if (updatedAttendance.StatusReason != default)
            existingEntity.StatusReason = updatedAttendance.StatusReason;
    }
}