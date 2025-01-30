namespace RockSchool.Data.Entities;

public class WorkingPeriodEntity
{
    public Guid Id { get; set; }
    public int Day { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
}