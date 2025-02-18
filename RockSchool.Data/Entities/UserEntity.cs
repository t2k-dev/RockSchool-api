using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities;

public class UserEntity
{
    [Key] public int UserId { get; set; }

    [Required] [MaxLength(100)] public string Login { get; set; }

    [Required] public string PasswordHash { get; set; }

    public int RoleId { get; set; }
    [ForeignKey(nameof(RoleId))] public RoleEntity Role { get; set; }

    public bool IsActive { get; set; }
}