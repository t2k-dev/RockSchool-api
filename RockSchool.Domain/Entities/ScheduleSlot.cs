using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockSchool.Domain.Entities
{
    public class ScheduleSlot
    {
        public Guid ScheduleSlotId { get; private set; }
        public Guid ScheduleId { get; private set; }
        public int RoomId { get; private set; }
        public Room? Room { get; private set; }
        public int WeekDay { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }

        
    }
}
