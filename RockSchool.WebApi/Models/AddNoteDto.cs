using System;

namespace RockSchool.WebApi.Models
{
    public class AddNoteDto
    {
        public int BranchId { get; set; }
        public string Description { get; set; }
        public DateTime? CompleteDate { get; set; }
    }
}
