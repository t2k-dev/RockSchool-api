namespace RockSchool.WebApi.Models.Attendances
{
    public class ParentAttendanceInfo : AttendanceInfo
    {
        public string Name { get; set; }
        public AttendanceInfo[] ChildAttendances { get; set; }
    }
}
