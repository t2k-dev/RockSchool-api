using RockSchool.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockSchool.BL.Dtos
{
    public class WorkingPeriodDto
    {
        public Guid WorkingPeriodId { get; set; }

        public Guid TeacherId { get; set; }

        public int WeekDay { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int RoomId { get; set; }
    }
}
