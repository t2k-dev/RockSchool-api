using RockSchool.Domain.Entities;

namespace RockSchool.BL.Models;

public class BusySlotsResultDto
{
    public int RoomId { get; set; }
    public string RoomName { get; set; } = string.Empty;
    public Attendance[] Attendances { get; set; } = [];
}
