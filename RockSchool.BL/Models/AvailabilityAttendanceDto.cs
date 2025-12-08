namespace RockSchool.BL.Models;

public class AvailabilityAttendanceDto
{
    public Guid TeacherId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Status { get; set; }
}