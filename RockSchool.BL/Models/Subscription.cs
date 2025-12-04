using RockSchool.BL.Dtos;
using RockSchool.Data.Enums;

namespace RockSchool.BL.Models
{
    public class Subscription
    {
        public Guid SubscriptionId { get; set; }

        public Guid? GroupId { get; set; }

        public Guid StudentId { get; set; }

        public virtual StudentDto Student { get; set; }

        public int AttendanceCount { get; set; }

        public int AttendanceLength { get; set; }

        public DateOnly StartDate { get; set; }

        public int Status { get; set; }

        public int DisciplineId { get; set; }

        public virtual DisciplineDto Discipline { get; set; }

        public int? TransactionId { get; set; }

        public Guid TeacherId { get; set; }

        public virtual TeacherDto Teacher { get; set; }

        public int BranchId { get; set; }

        public virtual BranchDto Branch { get; set; }

        public TrialStatus? TrialStatus { get; set; }

        public string? StatusReason { get; set; }
    }
}
