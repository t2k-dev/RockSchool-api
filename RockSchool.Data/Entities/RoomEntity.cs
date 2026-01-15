using System.ComponentModel.DataAnnotations;

namespace RockSchool.Data.Entities;

public class RoomEntity
{
    [Key]
    public int RoomId { get; set; }
    public int BranchId { get; set; }
    public BranchEntity Branch { get; set; }
    public string Name { get; set; }
    public int Status { get; set; }
    public bool IsActive { get; set; }
    public bool SupportsRent { get; set; }
    public bool SupportsRehearsal { get; set; }
}