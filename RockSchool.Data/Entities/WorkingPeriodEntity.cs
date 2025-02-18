using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities;

public class WorkingPeriodEntity
{
    [Key]
    public int WorkingPeriodId { get; set; }
    public int TeacherId { get; set; }
    
    [ForeignKey(nameof(TeacherId))]
    public TeacherEntity Teacher { get; set; }

    public string WeekDay { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }
}