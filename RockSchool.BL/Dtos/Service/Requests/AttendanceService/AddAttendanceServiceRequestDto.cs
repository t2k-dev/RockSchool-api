namespace RockSchool.BL.Dtos.Service.Requests.AttendanceService;

public class AddAttendanceServiceRequestDto
{
    public Guid StudentId { get; set; }
    public Guid TeacherId { get; set; }
    public int DisciplineId { get; set; }
    public int NumberOfAttendances { get; set; }
    public DateTime StartingDate { get; set; }
}