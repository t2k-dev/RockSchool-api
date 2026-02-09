using RockSchool.Domain.Enums;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Repositories;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.AttendanceService;

/// <summary>
/// Low level attendance service.
/// </summary>
public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendanceRepository;

    public AttendanceService(IAttendanceRepository attendanceRepository, IScheduleRepository scheduleRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    public async Task<Attendance[]> GetAllAttendancesAsync()
    {
        return await _attendanceRepository.GetAllAsync();
    }

    public async Task<Attendance?> GetAttendanceAsync(Guid attendanceId)
    {
        return await _attendanceRepository.GetAsync(attendanceId);
    }

    public async Task<Attendance[]> GetByBranchIdAsync(int branchId)
    {
        return await _attendanceRepository.GetByBranchIdAsync(branchId);
    }

    public async Task<Attendance[]> GetByRoomIdAsync(int roomId)
    {
        return await _attendanceRepository.GetByRoomIdAsync(roomId);
    }
    

    public async Task<Attendance[]?> GetAttendancesByTeacherIdForPeriodOfTime(Guid teacherId, DateTime startDate, DateTime endDate)
    {
        var attendanceEntities = await _attendanceRepository.GetByTeacherIdForPeriodOfTimeAsync(
            teacherId,
            DateTime.UtcNow.AddMonths(-1),
            DateTime.UtcNow.AddMonths(1)
        );

        return attendanceEntities;
    }

    public async Task<Attendance[]?> GetAttendancesByStudentId(Guid studentId)
    {
        return await _attendanceRepository.GetByStudentIdAsync(studentId);
    }

    public async Task<Attendance[]?> GetAttendancesBySubscriptionId(Guid subscriptionId)
    {
        return await _attendanceRepository.GetBySubscriptionIdAsync(subscriptionId);
    }

    public async Task AddAttendancesAsync(Attendance[] attendances)
    {
        await _attendanceRepository.AddRangeAsync(attendances.ToList());
    }

    public async Task<Guid> AddAttendanceAsync(Attendance attendance)
    {
        var attendanceId = await _attendanceRepository.AddAsync(attendance);
        return attendanceId;
    }

    public async Task UpdateAttendanceAsync(Attendance attendance)
    {
        throw new NullReferenceException("Not Implemented.");
        var existingEntity = await _attendanceRepository.GetAsync(attendance.AttendanceId);

        if (existingEntity == null)
            throw new NullReferenceException("AttendanceEntity not found.");


        await _attendanceRepository.UpdateAsync(existingEntity);
    }

    public async Task UpdateCommentAsync(Guid attendanceId, string comment)
    {
        var attendanceEntity = await _attendanceRepository.GetAsync(attendanceId);

        if (attendanceEntity == null)
            throw new NullReferenceException("AttendanceEntity not found.");

        attendanceEntity.AddComment(comment);

        await _attendanceRepository.UpdateAsync(attendanceEntity);
    }

    public async Task UpdateDateAndLocationAsync(Guid attendanceId, DateTime startDate, DateTime endDate, int roomId)
    {
        var attendanceEntity = await _attendanceRepository.GetAsync(attendanceId);

        if (attendanceEntity == null)
            throw new NullReferenceException("AttendanceEntity not found.");

        attendanceEntity.UpdateSchedule(startDate, endDate, roomId);

        await _attendanceRepository.UpdateAsync(attendanceEntity);
    }

    public async Task CancelFromDate(Guid subscriptionId, DateTime cancelDate)
    {
        var attendances = await GetAttendancesBySubscriptionId(subscriptionId);
        if (attendances == null)
        {
            return;
        }

        var attendancesToCancel = attendances.Where(a => a.StartDate >= cancelDate && a.Status == AttendanceStatus.New);
        foreach (var attendance in attendancesToCancel)
        {
            await _attendanceRepository.DeleteAsync(attendance.AttendanceId);
        }
    }
}