using System;

namespace RockSchool.WebApi.Models
{
    public class AttendanceInfo
    {
        public Guid AttendanceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public int RoomId { get; set; }
        public int DisciplineId { get; set; }
        public object Student { get; set; }
        public object Teacher { get; set; }

        /// <summary>
        /// Temp
        /// </summary>
        public bool IsTrial { get; set; }
    }
}
