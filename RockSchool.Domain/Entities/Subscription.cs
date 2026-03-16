using RockSchool.Domain.Enums;
using RockSchool.Domain.Repositories;
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
    public TrialDecision? TrialDecision { get; private set; }
    public Guid? TariffId { get; private set; }
    public Tariff? Tariff { get; private set; }
    public Guid? ScheduleId { get; private set; }
    public Schedule? Schedule { get; private set; }
    public SubscriptionType SubscriptionType { get; private set; }
    public decimal Price { get; private set; }
    public decimal FinalPrice { get; private set; }
    public decimal AmountOutstanding { get; private set; }
    public Guid? BaseSubscriptionId { get; private set; }

    private readonly List<Payment> _payments = new();
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

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
        Guid? baseSubscriptionsId = null,
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
            BaseSubscriptionId = baseSubscriptionsId,
            DisciplineId = disciplineId,
            TeacherId = teacherId,
            GroupId = groupId
        };
    }

    public static Subscription CreateTrial(
        Guid studentId,
        int branchId,
        DateOnly startDate,
        decimal price,
        decimal finalPrice,
        int disciplineId,
        Guid teacherId
        )
    {
        return new Subscription
        {
            SubscriptionId = Guid.NewGuid(),
            StudentId = studentId,
            BranchId = branchId,
            StartDate = startDate,
            AttendanceCount = 1,
            AttendanceLength = 60,
            AttendancesLeft = 1,
            SubscriptionType = SubscriptionType.TrialLesson,
            Price = price,
            FinalPrice = finalPrice,
            AmountOutstanding = finalPrice,
            Status = SubscriptionStatus.Draft,
            DisciplineId = disciplineId,
            TeacherId = teacherId,
        };
    }

    public void RecordPayment(Payment payment)
    {
        if (payment.Amount > AmountOutstanding)
            throw new InvalidOperationException("Payment amount exceeds outstanding balance");

        _payments.Add(payment);
        AmountOutstanding -= payment.Amount;

        if (AmountOutstanding == 0)
        {
            Status = SubscriptionStatus.Active;
        }
    }

    public void Cancel(string reason)
    {
        if (Status == SubscriptionStatus.Cancelled)
            throw new InvalidOperationException("Subscription is already cancelled");

        Status = SubscriptionStatus.Cancelled;
        StatusReason = reason;
    }

    public void AssignSchedule(Guid scheduleId)
    {
        ScheduleId = scheduleId;
    }

    public void ApplyDiscount(decimal discountAmount)
    {
        if (discountAmount < 0 || discountAmount > Price)
            throw new InvalidOperationException("Invalid discount amount");

        FinalPrice = Price - discountAmount;
        AmountOutstanding = FinalPrice;
    }

    // Trials
    public void CompleteTrial(TrialDecision trialDecision)
    {
        AttendancesLeft -= 1;
        Status = SubscriptionStatus.Completed;
        TrialDecision = trialDecision;
    }

    public void ReduceAttendanceCount()
    {
        AttendancesLeft -= 1;
    }
}
