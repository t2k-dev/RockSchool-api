namespace RockSchool.BL.Dtos.Service.Requests.ScheduleService;

public class AddScheduleServiceRequestDto
{
    public int StudentId { get; set; }
    public int WeekDay { get; set; }
    public string StartTime { get; set; }
    public int Duration { get; set; }
    public int DisciplineId { get; set; }
}