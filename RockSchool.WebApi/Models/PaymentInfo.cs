using System;

namespace RockSchool.WebApi.Models
{
    public class PaymentInfo
    {
        public Guid PaymentId { get; set; }

        public Decimal Amount { get; set; }

        public DateTime PaidOn { get; set; }

        public int PaymentType { get; set; }

        public Guid SubscriptionId { get; set; }
    }
}