using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.AttendanceService;

public interface IAttendanceService
{
    Task<AttendanceDto[]> GetAllAttendancesAsync();
    Task<AttendanceDto[]?> GetAttendancesByTeacherIdForPeriodOfTime(Guid teacherId, DateTime startDate, DateTime endDate);
    Task<AttendanceDto[]?> GetAttendancesByStudentId(Guid studentId);
    Task<AttendanceDto[]> GetByBranchIdAsync(int branchId);
    Task AddAttendancesToStudent(AttendanceDto attendanceDto);
    Task<Guid> AddTrialAttendanceAsync(AttendanceDto attendanceDto);
}