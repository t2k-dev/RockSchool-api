using RockSchool.Domain.Enums;

namespace RockSchool.Domain.Entities;

public class Payment
{
    public Guid PaymentId { get; private set; }
    public int Amount { get; private set; }
    public DateTime PaidOn { get; private set; }
    public TenderType PaymentType { get; private set; }

    private Payment() { }

    public static Payment Create(int amount, DateTime paidOn, TenderType paymentType)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero", nameof(amount));

        return new Payment
        {
            PaymentId = Guid.NewGuid(),
            Amount = amount,
            PaidOn = paidOn,
            PaymentType = paymentType
        };
    }

    public void UpdateAmount(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero", nameof(amount));

        Amount = amount;
    }

    public void UpdatePaymentDate(DateTime paidOn)
    {
        PaidOn = paidOn;
    }
}
