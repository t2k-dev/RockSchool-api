using System;

namespace RockSchool.WebApi.Models
{
    public class NoteInfo
    {
        public int BranchId { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime? ActualCompleteDate { get; set; }
        public int Status { get; set; }
    }
}
