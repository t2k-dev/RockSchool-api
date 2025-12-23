using RockSchool.Data.Enums;

namespace RockSchool.BL.Models
{
    public class Payment
    {
        public Guid PaymentId { get; set; }

        public int Amount { get; set; }

        public DateTime PaidOn { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}
