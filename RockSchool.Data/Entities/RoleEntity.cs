using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RockSchool.Data.Entities;

public class RoleEntity : IdentityRole<Guid>
{
    [Key] public int RoleId { get; set; }

    public string RoleName { get; set; }

    public bool IsActive { get; set; }
}