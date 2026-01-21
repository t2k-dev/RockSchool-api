using RockSchool.Data.Enums;

namespace RockSchool.BL.Models
{
    public class Subscription
    {
        public Guid SubscriptionId { get; set; }

        public Guid? GroupId { get; set; }

        public Guid StudentId { get; set; }

        public virtual Student Student { get; set; }

        public int AttendanceCount { get; set; }

        public int AttendanceLength { get; set; }

        public int AttendancesLeft { get; set; }

        public DateOnly StartDate { get; set; }

        public SubscriptionStatus Status { get; set; }

        public int? DisciplineId { get; set; }

        public virtual Discipline? Discipline { get; set; }

        public Guid? PaymentId { get; set; }

        public Guid? TeacherId { get; set; }

        public virtual Teacher? Teacher { get; set; }

        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }

        public TrialStatus? TrialStatus { get; set; }

        public string? StatusReason { get; set; }

        public SubscriptionType SubscriptionType { get; set; }

        public decimal Price { get; set; }

        public decimal FinalPrice { get; set; }

        public decimal AmountOutstanding { get; set; }

        public virtual ICollection<Schedule>? Schedules { get; set; }
    }
}
