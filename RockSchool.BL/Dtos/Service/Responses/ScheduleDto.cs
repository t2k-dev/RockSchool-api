using RockSchool.Data.Entities;

namespace RockSchool.BL.Dtos.Service.Responses;

public class ScheduleDto
{
    public int ScheduleId { get; set; }
    public int StudentId { get; set; }
    public StudentEntity StudentEntity { get; set; }
    public int WeekDay { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int DisciplineId { get; set; }
    public DisciplineEntity DisciplineEntity { get; set; }
}