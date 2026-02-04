using RockSchool.Domain.Enums;
using System;

namespace RockSchool.WebApi.Models.Notes
{
    public class NoteDto
    {
        public Guid NoteId { get; set; }
        public int? BranchId { get; set; }
        public string Description { get; set; }
        public NoteStatus Status { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime? ActualCompleteDate { get; set; }
        public string Comment { get; set; }
    }
}
