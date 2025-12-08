using System;

namespace RockSchool.WebApi.Models
{
    public class DeclineAttendanceRequest
    {
        public Guid SubscriptionId { get; set; }
        public string StatusReason { get; set; }
        public string Comment { get; set; }
    }
}
