using System;

namespace RockSchool.WebApi.Models.Attendances
{
    // TODO rename to Info
    public class AttendanceModel
    {
        public Guid AttendanceId { get; set; }
        public int AttendanceType { get; set; }
        public int? DisciplineId { get; set; }
        public int Status { get; set; }
        public string StatusReason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RoomId { get; set; }

        public object Teacher { get; set; }
        public bool IsCompleted { get; set; }
        public Guid? GroupId { get; set; }
        public string Comment { get; set; }
        public AttendanceStudents[] Students { get; set; }
    }
}
