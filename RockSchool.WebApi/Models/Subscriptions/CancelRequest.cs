using System;

namespace RockSchool.WebApi.Models.Subscriptions
{
    public class CancelRequest
    {
        public DateTime CancelDate { get; set; }
        public string CancelReason { get; set; }
    }
}
