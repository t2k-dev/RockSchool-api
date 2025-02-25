﻿using RockSchool.Data.Entities;
using RockSchool.Data.Enums;

namespace RockSchool.BL.Dtos;

public class AttendanceDto
{
    public Guid AttendanceId { get; set; }
    public Guid StudentId { get; set; }
    public virtual StudentEntity Student { get; set; }
    public Guid SubscriptionId { get; set; }
    public virtual SubscriptionEntity Subscription { get; set; }
    public int DisciplineId { get; set; }
    public virtual DisciplineEntity Discipline { get; set; }
    public Guid TeacherId { get; set; }
    public virtual TeacherEntity Teacher { get; set; }
    public AttendanceStatus Status { get; set; }
    public string StatusReason { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsGroup { get; set; }
    public int RoomId { get; set; }
    public virtual RoomEntity Room { get; set; }
    public string Comment { get; set; }
    public int NumberOfAttendances { get; set; }
}