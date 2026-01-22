using RockSchool.Data.Enums;

namespace RockSchool.BL.Models;

public class Tariff
{
    public Guid TariffId { get; set; }

    public decimal Amount { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int? DisciplineId { get; set; }

    public Discipline? Discipline { get; set; }

    public int AttendanceLength { get; set; }

    public int AttendanceCount { get; set; }

    public SubscriptionType SubscriptionType { get; set; }
}