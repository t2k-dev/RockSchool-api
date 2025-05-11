using RockSchool.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockSchool.Data.Enums;

namespace RockSchool.BL.Dtos
{
    public class SubscriptionDto
    {
        public Guid SubscriptionId { get; set; }

        public bool IsGroup { get; set; }

        public Guid StudentId { get; set; }

        public virtual StudentDto Student { get; set; }

        public int? AttendanceCount { get; set; }

        public int? AttendanceLength { get; set; }

        public DateTime StartDate { get; set; }

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
