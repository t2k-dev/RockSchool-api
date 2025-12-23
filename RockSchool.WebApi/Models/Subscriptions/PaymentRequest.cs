using System;

namespace RockSchool.WebApi.Models.Subscriptions
{
    public class PaymentRequest
    {
        public int Amount { get; set; }
        public int PaymentType { get; set; }
        public DateTime PaidOn { get; set; }

    }
}
