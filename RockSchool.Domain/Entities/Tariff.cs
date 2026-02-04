using RockSchool.Domain.Enums;

namespace RockSchool.Domain.Entities;

public class Tariff
{
    public Guid TariffId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public int? DisciplineId { get; private set; }
    public Discipline? Discipline { get; private set; }
    public int AttendanceLength { get; private set; }
    public int AttendanceCount { get; private set; }
    public SubscriptionType SubscriptionType { get; private set; }

    private Tariff() { }

    public static Tariff Create(
        decimal amount,
        DateTime startDate,
        DateTime endDate,
        int attendanceLength,
        int attendanceCount,
        SubscriptionType subscriptionType,
        int? disciplineId = null)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero", nameof(amount));

        if (startDate >= endDate)
            throw new ArgumentException("Start date must be before end date");

        if (attendanceLength <= 0)
            throw new ArgumentException("Attendance length must be greater than zero", nameof(attendanceLength));

        if (attendanceCount <= 0)
            throw new ArgumentException("Attendance count must be greater than zero", nameof(attendanceCount));

        return new Tariff
        {
            TariffId = Guid.NewGuid(),
            Amount = amount,
            StartDate = startDate,
            EndDate = endDate,
            AttendanceLength = attendanceLength,
            AttendanceCount = attendanceCount,
            SubscriptionType = subscriptionType,
            DisciplineId = disciplineId
        };
    }

    public void UpdateAmount(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero", nameof(amount));

        Amount = amount;
    }

    public void UpdateDates(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate)
            throw new ArgumentException("Start date must be before end date");

        StartDate = startDate;
        EndDate = endDate;
    }
}