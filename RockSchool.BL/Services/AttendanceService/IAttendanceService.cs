using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.AttendanceService;

public interface IAttendanceService
{
    Task<AttendanceDto[]> GetAllAttendancesAsync();
    Task<AttendanceDto[]?> GetAttendancesByTeacherIdForPeriodOfTime(Guid teacherId, DateTime startDate, DateTime endDate);
    Task AddAttendancesToStudent(AttendanceDto attendanceDto);
    Task AddTrialAttendance(AttendanceDto attendanceDto);
}