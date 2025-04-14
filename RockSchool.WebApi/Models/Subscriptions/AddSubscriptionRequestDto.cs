using System;
using RockSchool.BL.Dtos;

namespace RockSchool.WebApi.Models.Subscriptions;

public class AddSubscriptionRequestDto
{
    public int DisciplineId { get; set; }
    public Guid TeacherId { get; set; }
    public Guid StudentId { get; set; }
    public int BranchId { get; set; }
    public DateTime StartDate { get; set; }
    public ScheduleDto Schedule { get; set; }
}