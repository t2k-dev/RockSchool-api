using RockSchool.BL.Models.Dtos;
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Home;

public class HomeDetailsWithAttendeesDto
{
    public Branch Branch { get; set; } = null!;
    public AttendanceWithAttendeesDto[] Attendances { get; set; } = [];
    public NoteDto[] Notes { get; set; } = [];
}
