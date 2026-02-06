using RockSchool.Domain.Enums;
using RockSchool.Domain.Students;
using RockSchool.Domain.Teachers;

namespace RockSchool.Domain.Entities;

public class Subscription
{
    public Guid SubscriptionId { get; private set; }
    public Guid? GroupId { get; private set; }
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; }
    public int AttendanceCount { get; private set; }
    public int AttendanceLength { get; private set; }
    public int AttendancesLeft { get; private set; }
    public DateOnly StartDate { get; private set; }
    public SubscriptionStatus Status { get; private set; }
    public string? StatusReason { get; private set; }
    public int? DisciplineId { get; private set; }
    public Discipline? Discipline { get; private set; }
    public Guid? TeacherId { get; private set; }
    public Teacher? Teacher { get; private set; }
    public int BranchId { get; private set; }
    public Branch Branch { get; private set; }
    public TrialStatus? TrialStatus { get; private set; }
    public Guid? TariffId { get; private set; }
    public Tariff? Tariff { get; private set; }
    public SubscriptionType SubscriptionType { get; private set; }
    public decimal Price { get; private set; }
    public decimal FinalPrice { get; private set; }
    public decimal AmountOutstanding { get; private set; }

    private readonly List<Schedule> _schedules = new();
    public IReadOnlyCollection<Schedule> Schedules => _schedules.AsReadOnly();

    private readonly List<Tender> _tenders = new();
    public IReadOnlyCollection<Tender> Tenders => _tenders.AsReadOnly();

    // Constructor for EF
    private Subscription() { }

    // Factory method for creation
    public static Subscription Create(
        Guid studentId,
        int branchId,
        DateOnly startDate,
        int attendanceCount,
        int attendanceLength,
        SubscriptionType subscriptionType,
        decimal price,
        decimal finalPrice,
        int? disciplineId = null,
        Guid? teacherId = null,
        Guid? groupId = null)
    {
        return new Subscription
        {
            SubscriptionId = Guid.NewGuid(),
            StudentId = studentId,
            BranchId = branchId,
            StartDate = startDate,
            AttendanceCount = attendanceCount,
            AttendanceLength = attendanceLength,
            AttendancesLeft = attendanceCount,
            SubscriptionType = subscriptionType,
            Price = price,
            FinalPrice = finalPrice,
            AmountOutstanding = finalPrice,
            Status = SubscriptionStatus.Draft,
            DisciplineId = disciplineId,
            TeacherId = teacherId,
            GroupId = groupId
        };
    }

    public void RecordPayment(Tender tender)
    {
        if (tender.Amount > AmountOutstanding)
            throw new InvalidOperationException("Payment amount exceeds outstanding balance");

        _tenders.Add(tender);
        AmountOutstanding -= tender.Amount;
    }

    public void Cancel(string reason)
    {
        if (Status == SubscriptionStatus.Cancelled)
            throw new InvalidOperationException("Subscription is already cancelled");

        Status = SubscriptionStatus.Cancelled;
        StatusReason = reason;
    }

    public void AddSchedule(Schedule schedule)
    {
        _schedules.Add(schedule);
    }

    public void ApplyDiscount(decimal discountAmount)
    {
        if (discountAmount < 0 || discountAmount > Price)
            throw new InvalidOperationException("Invalid discount amount");

        FinalPrice = Price - discountAmount;
        AmountOutstanding = FinalPrice;
    }
}
