﻿namespace RockSchool.BL.Dtos;

public class ScheduledWorkingPeriodDto
{
    public Guid TeacherId { get; set; }
    public Guid ScheduledWorkingPeriodId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int RoomId { get; set; }
}