using System;

namespace RockSchool.WebApi.Models
{
    public class MarkCompleteDto
    {
        public Guid NoteId { get; set; }
        public int Status { get; set; } 
    }
}
