using System;
using RockSchool.Domain.Enums;

namespace RockSchool.WebApi.Models;

public class AddTariffDto
{
    public required decimal Amount { get; set; }

    public required int AttendanceCount { get; set; }

    public required int AttendanceLength { get; set; }

    public int? DisciplineId { get; set; }

    public required DateTime EndDate { get; set; }

    public required DateTime StartDate { get; set; }

    public required SubscriptionType SubscriptionType { get; set; }
}