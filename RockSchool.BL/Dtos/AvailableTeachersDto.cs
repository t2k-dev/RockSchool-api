namespace RockSchool.BL.Dtos
{
    public class AvailableTeachersDto
    {
        public AvailableTeacherDto[] AvailableTeachers { get; set; } = Array.Empty<AvailableTeacherDto>();
        public ScheduledWorkingPeriodDto[] ScheduledWorkingPeriods { get; set; } = Array.Empty<ScheduledWorkingPeriodDto>();
        public AvailabilityAttendanceDto[] Attendancies { get; set; } = Array.Empty<AvailabilityAttendanceDto>();
    }
}