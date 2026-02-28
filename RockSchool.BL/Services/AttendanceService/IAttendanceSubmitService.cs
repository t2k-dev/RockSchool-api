namespace RockSchool.BL.Services.AttendanceService
{
    public interface IAttendanceSubmitService
    {
        Task SubmitAttendance(Guid attendanceId, int status, string statusReason, string comment);
    }
}
