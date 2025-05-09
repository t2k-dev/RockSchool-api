﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities;

public class WorkingPeriodEntity
{
    [Key]
    public Guid WorkingPeriodId { get; set; }

    public Guid TeacherId { get; set; }

    public TeacherEntity Teacher { get; set; }

    public int WeekDay { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public int RoomId { get; set; }

    public RoomEntity Room { get; set; }

    public ICollection<ScheduledWorkingPeriodEntity>? ScheduledWorkingPeriods { get; set; }
}