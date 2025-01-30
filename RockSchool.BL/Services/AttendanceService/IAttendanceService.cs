using RockSchool.BL.Dtos.Service.Requests.AttendanceService;
using RockSchool.BL.Dtos.Service.Responses;

namespace RockSchool.BL.Services.AttendanceService;

public interface IAttendanceService
{
    Task<AttendanceDto[]> GetAllAttendancesAsync();
    Task AddAttendancesToStudent(AddAttendanceServiceRequestDto  addAttendanceServiceRequestDto);
}