using RockSchool.Data.Enums;

namespace RockSchool.BL.Dtos
{
    public class NoteDto
    {
        public Guid NoteId { get; set; }
        public int? BranchId { get; set; }
        public string Description { get; set; }
        public NoteStatus Status { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime? ActualCompleteDate { get; set; }
    }
}
