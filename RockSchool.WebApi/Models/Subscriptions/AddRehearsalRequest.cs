using System;

namespace RockSchool.WebApi.Models.Subscriptions
{
    public class AddRehearsalRequest
    {
        public Guid StudentId { get; set; }
        public Guid BandId { get; set; }
        public Guid TariffId { get; set; }
    }
}
