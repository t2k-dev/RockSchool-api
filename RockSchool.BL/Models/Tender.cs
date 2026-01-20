using RockSchool.Data.Enums;

namespace RockSchool.BL.Models
{
    public class Tender
    {
        public Guid TenderId { get; set; }

        public int Amount { get; set; }

        public DateTime PaidOn { get; set; }

        public TenderType TenderType { get; set; }

        public Guid SubscriptionId { get; set; }
    }
}
