namespace RockSchool.BL.Models;

public class Room
{
    public int RoomId { get; set; }

    public int BranchId { get; set; }

    public Branch Branch { get; set; }

    public string Name { get; set; }

    public int Status { get; set; }

    public bool IsActive { get; set; }

    public bool SupportsRent { get; set; }

    public bool SupportsRehearsal { get; set; }
}