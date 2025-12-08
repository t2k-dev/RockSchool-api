using System.ComponentModel.DataAnnotations;

namespace RockSchool.BL.Models;

public class RoomDto
{
    [Key]
    public int RoomId { get; set; }

    public int BranchId { get; set; }

    public BranchDto Branch { get; set; }

    public string Name { get; set; }

    public int Status { get; set; }

    public bool IsActive { get; set; }
}