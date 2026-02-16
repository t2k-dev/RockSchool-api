using RockSchool.BL.Models.Dtos;

namespace RockSchool.BL.Services.AttendanceService;

public interface IAttendanceQueryService
{
    Task<AttendanceWithAttendeesDto[]> GetByTeacherIdForPeriodAsync(Guid teacherId, DateTime startDate, DateTime endDate);
}
