using System.Collections.Generic;

namespace RockSchool.WebApi.Models.Attendances
{
    public class SubmitGroupAttendanceRequest
    {
        public List<AttendanceInfo> ChildAttendances { get; set; }
        public string Comment { get; set; }
    }
}
