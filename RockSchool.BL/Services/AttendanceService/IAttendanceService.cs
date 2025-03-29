using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.AttendanceService;

public interface IAttendanceService
{
    Task<AttendanceDto[]> GetAllAttendancesAsync();
    Task AddAttendancesToStudent(AttendanceDto attendanceDto);
    Task<AttendanceDto[]?> GetAttendanciesByTeacherIdForPeriodOfTime(Guid teacherId, DateTime startDate, DateTime endDate);
}