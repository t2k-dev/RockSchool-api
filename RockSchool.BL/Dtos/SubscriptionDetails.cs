using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockSchool.BL.Dtos
{
    public class SubscriptionDetails
    {
        public int DisciplineId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid StudentId { get; set; }
        public int BranchId { get; set; }
        public DateTime StartDate { get; set; }
        public int AttendanceCount { get; set; }
        public int AttendanceLength { get; set; }
    }
}
