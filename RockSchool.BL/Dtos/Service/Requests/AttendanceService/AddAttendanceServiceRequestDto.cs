namespace RockSchool.BL.Dtos.Service.Requests.AttendanceService;

public class AddAttendanceServiceRequestDto
{
    public int StudentId { get; set; }
    public int TeacherId { get; set; }
    public int DisciplineId { get; set; }
    public int NumberOfAttendances { get; set; }
    public DateTime StartingDate { get; set; }
}