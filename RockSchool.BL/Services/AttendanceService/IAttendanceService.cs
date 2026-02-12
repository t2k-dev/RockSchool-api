using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.AttendanceService;

public interface IAttendanceService
{
    Task<Attendance[]> GetAllAttendancesAsync();
    
    Task<Attendance?> GetAttendanceAsync(Guid attendanceId);
    Task<Attendance[]> GetByRoomIdAsync(int roomId);
    /*Task<Attendance[]?> GetAttendancesByTeacherIdForPeriodOfTime(Guid teacherId, DateTime startDate, DateTime endDate);
    Task<Attendance[]?> GetAttendancesByStudentId(Guid studentId);
    Task<Attendance[]?> GetAttendancesBySubscriptionId(Guid subscriptionId);
    Task<Attendance[]> GetByBranchIdAsync(int branchId);
    
    Task<Guid> AddAttendanceAsync(Attendance attendanceDto);
    Task AddAttendancesAsync(Attendance[] attendances);
    Task UpdateAttendanceAsync(Attendance attendanceDto);
    Task UpdateCommentAsync(Guid attendanceId, string comment);
    Task UpdateDateAndLocationAsync(Guid attendanceId, DateTime startDate, DateTime endDate, int roomId);
    Task CancelFromDate(Guid subscriptionId, DateTime cancelDate);*/
}