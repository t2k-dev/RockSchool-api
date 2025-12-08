using RockSchool.BL.Models;
using RockSchool.WebApi.Models.Attendances;

namespace RockSchool.WebApi.Models
{
    public class HomeScreenDetails
    {
        //TODO: Refactor
        public Branch Branch { get; set; }
        public ParentAttendanceInfo[] Attendances { get; set; }
        public object[] Rooms { get; set; }
        public Note[] Notes { get; set; }
    }
}
