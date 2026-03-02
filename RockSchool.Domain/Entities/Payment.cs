using RockSchool.Domain.Enums;

namespace RockSchool.Domain.Entities;

public class Payment
{
    public Guid PaymentId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime PaidOn { get; private set; }
    public PaymentType PaymentType { get; private set; }
    public Guid SubscriptionId { get; private set; }
    public Subscription Subscription { get; private set; }

    private Payment() { }

    public static Payment Create(
        decimal amount,
        DateTime paidOn,
        PaymentType paymentType,
        Guid subscriptionId)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Payment amount must be greater than zero");

        return new Payment
        {
            PaymentId = Guid.NewGuid(),
            Amount = amount,
            PaidOn = paidOn,
            PaymentType = paymentType,
            SubscriptionId = subscriptionId
        };
    }
}
