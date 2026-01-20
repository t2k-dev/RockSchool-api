using RockSchool.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities
{
    public class TenderEntity
    {
        [Key]
        public Guid TenderId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaidOn { get; set; }

        public TenderType TenderType { get; set; }

        public Guid SubscriptionId { get; set; }

        [ForeignKey(nameof(SubscriptionId))]
        public SubscriptionEntity Subscription { get; set; }
    }
}
