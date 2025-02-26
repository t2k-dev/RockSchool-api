using RockSchool.Data.Entities;

namespace RockSchool.BL.Dtos;

public class BranchDto
{
    public int BranchId { get; set; }

    public required string Name { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public ICollection<RoomEntity>? Rooms { get; set; }
}