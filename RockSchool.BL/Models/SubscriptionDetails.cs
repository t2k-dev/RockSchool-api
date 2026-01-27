namespace RockSchool.BL.Models
{
    public class SubscriptionDetails
    {
        public int? DisciplineId { get; set; }
        public Guid? TeacherId { get; set; }
        
        // DEV?
        public Guid StudentId { get; set; }
        public int BranchId { get; set; }
        public DateOnly StartDate { get; set; }
        public int AttendanceCount { get; set; }
        public int AttendanceLength { get; set; }
        public decimal Price { get; set; }
        public decimal FinalPrice { get; set; }
        public Guid TariffId { get; set; }
    }
}
