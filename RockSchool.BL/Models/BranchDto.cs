namespace RockSchool.BL.Models;

public class BranchDto
{
    public int BranchId { get; set; }

    public required string Name { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public ICollection<RoomDto>? Rooms { get; set; }
}