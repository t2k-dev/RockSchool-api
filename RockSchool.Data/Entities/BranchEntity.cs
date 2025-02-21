using System.ComponentModel.DataAnnotations;

namespace RockSchool.Data.Entities;

public class BranchEntity
{
    [Key]
    public int BranchId { get; set; }

    public required string Name { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public ICollection<RoomEntity>? Rooms { get; set; }
}