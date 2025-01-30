namespace RockSchool.Data.Entities;

public class WorkingHoursEntity
{
    public Guid Id { get; set; }
    public WorkingPeriodEntity[] WorkingPeriods { get; set; }
    public WorkingPeriodEntity[] Breaks { get; set; }
}