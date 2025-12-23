using System.ComponentModel.DataAnnotations;

namespace RockSchool.Data.Entities
{
    public class PaymentEntity
    {
        [Key]
        public Guid PaymentId { get; set; }

        public int Amount { get; set; }

        public DateTime PaidOn { get; set; }

        public int PaymentType { get; set; }
    }
}
