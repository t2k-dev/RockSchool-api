namespace RockSchool.BL.Models
{
    public class TrialRequestDto
    {
        public int DisciplineId { get; set; }
        public int BranchId { get; set; }
        public Guid TeacherId { get; set; }
        public DateTime TrialDate { get; set; }
        public int RoomId { get; set; }
        public Student Student { get; set; }

    }
}
