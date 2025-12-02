using RockSchool.BL.Dtos;
using RockSchool.Data.Enums;

namespace RockSchool.BL.Services.AttendanceService;

public interface IAttendanceService
{
    Task<AttendanceDto[]> GetAllAttendancesAsync();
    Task<AttendanceDto?> GetAttendanceAsync(Guid attendanceId);
    Task<AttendanceDto[]?> GetAttendancesByTeacherIdForPeriodOfTime(Guid teacherId, DateTime startDate, DateTime endDate);
    Task<AttendanceDto[]?> GetAttendancesByStudentId(Guid studentId);
    Task<AttendanceDto[]> GetByBranchIdAsync(int branchId);
    Task<Guid> AddAttendanceAsync(AttendanceDto attendanceDto);
    Task AddAttendancesAsync(AttendanceDto[] attendances);
    Task UpdateAttendanceAsync(AttendanceDto attendanceDto);
    Task UpdateStatusAsync(Guid attendanceId, int status);
    Task UpdateAttendances(List<AttendanceDto> attendances);
}