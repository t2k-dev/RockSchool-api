namespace RockSchool.BL.Dtos;

public class AvailableTeacherDto
{
    public Guid TeacherId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int WorkLoad { get; set; }

    public ScheduledWorkingPeriodDto[] ScheduledWorkingPeriods { get; set; } = Array.Empty<ScheduledWorkingPeriodDto>();
    public AvailabilityAttendanceDto[] Attendancies { get; set; } = Array.Empty<AvailabilityAttendanceDto>();
}