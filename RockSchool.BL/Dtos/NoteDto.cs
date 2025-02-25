namespace RockSchool.BL.Dtos
{
    public class NoteDto
    {
        public Guid NoteId { get; set; }
        public int? BranchId { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
