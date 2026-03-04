using RockSchool.Domain.Enums;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Services.AttendeeService;

public class AttendeeService(
    IAttendanceRepository attendanceRepository,
    IUnitOfWork unitOfWork) : IAttendeeService
{
    public async Task<bool> UpdateStatus(Guid attendanceId, Guid attendeeId, int attendeeStatus)
    {
        var attendance = await attendanceRepository.GetAsync(attendanceId);

        var attendee = attendance.Attendees.SingleOrDefault(a => a.AttendeeId == attendeeId);

        switch ((AttendeeStatus)attendeeStatus)
        {
            case AttendeeStatus.Attended:
                attendee.MarkAsAttended();
                break;
            case AttendeeStatus.Missed:
                attendee.MarkAsMissed();
                break;
        }

        if (attendance.Attendees.All(a => a.Status == AttendeeStatus.Missed))
        {
            attendance.MarkAsMissed();
        }
        else if (attendance.Attendees.All(a => a.Status != AttendeeStatus.New))
        {
            attendance.MarkAsAttended();
        }

        attendanceRepository.Update(attendance);

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
