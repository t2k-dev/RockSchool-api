using RockSchool.BL.Models;
using RockSchool.WebApi.Models.Attendances;

namespace RockSchool.WebApi.Models
{
    public class RentableRoomDto
    {
        public string Name { get; set; }
        
        public int  Id { get; set; }

        public AttendanceInfo[] Attendances { get; set; }
    }
}
