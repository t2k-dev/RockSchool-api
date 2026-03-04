using System;

namespace RockSchool.WebApi.Models
{
    public class SubmitAttendeeRequest
    {
        public Guid AttendeeId { get; set; }
        public int AttendeeStatus { get; set; }
        public string Comment { get; set; }
    }
}
