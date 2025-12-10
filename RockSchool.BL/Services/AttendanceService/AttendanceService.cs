using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.Data.Entities;
using RockSchool.Data.Enums;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.AttendanceService;

/// <summary>
/// Low level attendance service.
/// </summary>
public class AttendanceService : IAttendanceService
{
    private readonly AttendanceRepository _attendanceRepository;

    public AttendanceService(AttendanceRepository attendanceRepository, ScheduleRepository scheduleRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    public async Task<Attendance[]> GetAllAttendancesAsync()
    {
        var attendances = await _attendanceRepository.GetAllAsync();

        var attendancesDto = attendances.Select(a => new Attendance
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

    public async Task<Attendance?> GetAttendanceAsync(Guid attendanceId)
    {
        var attendanceEntity = await _attendanceRepository.GetAsync(attendanceId);
        return attendanceEntity?.ToModel();
    }

    public async Task<Attendance[]> GetByBranchIdAsync(int branchId)
    {
        var attendances = await _attendanceRepository.GetByBranchIdAsync(branchId);
        return attendances.ToDto();
    }

    public async Task<Attendance[]?> GetAttendancesByTeacherIdForPeriodOfTime(Guid teacherId, DateTime startDate, DateTime endDate)
    {
        var attendanceEntities = await _attendanceRepository.GetByTeacherIdForPeriodOfTimeAsync(
            teacherId,
            DateTime.UtcNow.AddMonths(-1),
            DateTime.UtcNow.AddMonths(1)
        );

        return attendanceEntities?.ToDto();
    }

    public async Task<Attendance[]?> GetAttendancesByStudentId(Guid studentId)
    {
        var attendanceEntities = await _attendanceRepository.GetByStudentIdAsync(studentId);

        return attendanceEntities?.ToDto();
    }

    public async Task<Attendance[]?> GetAttendancesBySubscriptionId(Guid subscriptionId)
    {
        var attendanceEntities = await _attendanceRepository.GetBySubscriptionIdAsync(subscriptionId);

        return attendanceEntities?.ToDto();
    }

    public async Task AddAttendancesAsync(Attendance[] attendances)
    {
        var attendanceEntities = attendances.ToEntities();
        await _attendanceRepository.AddRangeAsync(attendanceEntities);

    }

    public async Task<Guid> AddAttendanceAsync(Attendance attendance)
    {
        var attendanceEntity = attendance.ToEntity();
    
        var attendanceId = await _attendanceRepository.AddAsync(attendanceEntity);
        return attendanceId;
    }

    public async Task UpdateAttendanceAsync(Attendance attendance)
    {
        var existingEntity = await _attendanceRepository.GetAsync(attendance.AttendanceId);

        if (existingEntity == null)
            throw new NullReferenceException("AttendanceEntity not found.");

        ModifyAttendanceAttributes(attendance, existingEntity);

        await _attendanceRepository.UpdateAsync(existingEntity);
    }

    public async Task UpdateCommentAsync(Guid attendanceId, string comment)
    {
        var attendanceEntity = await _attendanceRepository.GetAsync(attendanceId);

        if (attendanceEntity == null)
            throw new NullReferenceException("AttendanceEntity not found.");

        attendanceEntity.Comment = comment;

        await _attendanceRepository.UpdateAsync(attendanceEntity);
    }

    public async Task UpdateDateAndLocationAsync(Guid attendanceId, DateTime startDate, DateTime endDate, int roomId)
    {
        var attendanceEntity = await _attendanceRepository.GetAsync(attendanceId);

        if (attendanceEntity == null)
            throw new NullReferenceException("AttendanceEntity not found.");

        attendanceEntity.StartDate = startDate;
        attendanceEntity.EndDate = endDate;
        attendanceEntity.RoomId = roomId;

        await _attendanceRepository.UpdateAsync(attendanceEntity);
    }

    public async Task UpdateStatusAsync(Guid attendanceId, int status)
    {
        // TODO: discuss implementation
        var attendanceEntity = await _attendanceRepository.GetAsync(attendanceId);

        if (attendanceEntity == null)
            throw new NullReferenceException("AttendanceEntity not found.");

        attendanceEntity.Status = (AttendanceStatus)status;

        await _attendanceRepository.UpdateAsync(attendanceEntity);
    }

    private static void ModifyAttendanceAttributes(Attendance updatedAttendance, AttendanceEntity existingEntity)
    {
        if (updatedAttendance.Status != default)
            existingEntity.Status = updatedAttendance.Status;

        if (updatedAttendance.StatusReason != default)
            existingEntity.StatusReason = updatedAttendance.StatusReason;

        if (updatedAttendance.StartDate != default)
            existingEntity.StartDate = updatedAttendance.StartDate;

        if (updatedAttendance.EndDate != default)
            existingEntity.EndDate = updatedAttendance.EndDate;

        if (updatedAttendance.RoomId != default)
            existingEntity.RoomId = updatedAttendance.RoomId;
    }
}