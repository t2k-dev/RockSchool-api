using RockSchool.BL.Dtos;
using RockSchool.WebApi.Models.Attendances;

namespace RockSchool.WebApi.Models
{
    public class HomeScreenDetails
    {
        //TODO: Refactor
        public BranchDto Branch { get; set; }
        public ParentAttendanceInfo[] Attendances { get; set; }
        public object[] Rooms { get; set; }
        public Note[] Notes { get; set; }
    }
}
