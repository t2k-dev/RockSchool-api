using System;

namespace RockSchool.WebApi.Models
{
    public class TenderInfo
    {
        public Guid TenderId { get; set; }

        public Decimal Amount { get; set; }

        public DateTime PaidOn { get; set; }

        public int TenderType { get; set; }

        public Guid SubscriptionId { get; set; }
    }
}