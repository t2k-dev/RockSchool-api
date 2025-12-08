using RockSchool.BL.Models;
using RockSchool.Data.Enums;

namespace RockSchool.BL.Services.AttendanceService;

public interface IAttendanceService
{
    Task<Attendance[]> GetAllAttendancesAsync();
    Task<Attendance?> GetAttendanceAsync(Guid attendanceId);
    Task<Attendance[]?> GetAttendancesByTeacherIdForPeriodOfTime(Guid teacherId, DateTime startDate, DateTime endDate);
    Task<Attendance[]?> GetAttendancesByStudentId(Guid studentId);
    Task<Attendance[]> GetByBranchIdAsync(int branchId);
    Task<Guid> AddAttendanceAsync(Attendance attendanceDto);
    Task AddAttendancesAsync(Attendance[] attendances);
    Task UpdateAttendanceAsync(Attendance attendanceDto);
    Task UpdateStatusAsync(Guid attendanceId, int status);
}