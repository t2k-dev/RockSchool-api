using RockSchool.BL.Dtos;

namespace RockSchool.WebApi.Models
{
    public class HomeScreenDetails
    {
        //TODO: Refactor
        public BranchDto Branch { get; set; }
        public AttendanceInfo[] Attendances { get; set; }
        public object[] Rooms { get; set; }
        public NoteDto[] Notes { get; set; }
    }
}
