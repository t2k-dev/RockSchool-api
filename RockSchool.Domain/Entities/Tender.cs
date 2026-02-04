using RockSchool.Domain.Enums;

namespace RockSchool.Domain.Entities;

public class Tender
{
    public Guid TenderId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime PaidOn { get; private set; }
    public TenderType TenderType { get; private set; }
    public Guid SubscriptionId { get; private set; }
    public Subscription Subscription { get; private set; }

    private Tender() { }

    public static Tender Create(
        decimal amount,
        DateTime paidOn,
        TenderType tenderType,
        Guid subscriptionId)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Payment amount must be greater than zero");

        return new Tender
        {
            TenderId = Guid.NewGuid(),
            Amount = amount,
            PaidOn = paidOn,
            TenderType = tenderType,
            SubscriptionId = subscriptionId
        };
    }
}
