using RockSchool.Domain.Students;

namespace RockSchool.BL.Subscriptions.Trial
{
    public class AddTrialDto
    {
        public int DisciplineId { get; set; }
        public int BranchId { get; set; }
        public Guid TeacherId { get; set; }
        public DateTime TrialDate { get; set; }
        public int RoomId { get; set; }
        public Guid TariffId { get; set; }
        public Guid StudentId { get; set; }
    }
}
