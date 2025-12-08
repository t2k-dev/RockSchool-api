namespace RockSchool.BL.Services.AttendanceService
{
    public interface IAttendanceSubmitService
    {
        Task SubmitAttendance(Guid attendanceId, int status, string statusReason, string comment);
        Task AcceptTrial(Guid attendanceId, string statusReason, string comment);
        Task DeclineTrial(Guid attendanceId, string statusReason, string comment);
        Task MissedTrial(Guid attendanceId, string statusReason, string comment);
    }
}
