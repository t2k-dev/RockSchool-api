using RockSchool.BL.Models.Dtos;
using RockSchool.Domain.Entities;

namespace RockSchool.WebApi.Models
{
    public class HomeScreenDetails
    {
        public Branch Branch { get; set; } = null!;
        public AttendanceWithAttendeesDto[] Attendances { get; set; } = [];
        public object[] Rooms { get; set; } = [];
        public NoteDto[] Notes { get; set; } = [];
    }
}
