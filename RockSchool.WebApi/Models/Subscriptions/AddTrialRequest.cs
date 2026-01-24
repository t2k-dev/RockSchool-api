using System;
using RockSchool.WebApi.Models.Students;

namespace RockSchool.WebApi.Models.Subscriptions
{
    public class AddTrialRequest
    {
        public int DisciplineId { get; set; }
        public int BranchId { get; set; }
        public Guid TeacherId { get; set; }
        public DateTime TrialDate { get; set; }
        public int RoomId { get; set; }
        public Guid TariffId { get; set; }
        public StudentInfo Student { get; set; }
    }
}
