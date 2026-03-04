namespace RockSchool.BL.Services.AttendeeService;

public interface IAttendeeService
{
    Task<bool> UpdateStatus(Guid attendanceId, Guid attendeeId, int attendeeStatus);
}
