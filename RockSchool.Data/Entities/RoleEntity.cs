using System.ComponentModel.DataAnnotations;

namespace RockSchool.Data.Entities;

public class RoleEntity
{
    [Key] public int RoleId { get; set; }

    public string RoleName { get; set; }

    public bool IsActive { get; set; }
}