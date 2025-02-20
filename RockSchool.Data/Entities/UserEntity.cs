using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities;

public class UserEntity
{
    [Key]
    public Guid UserId { get; set; }

    public string Login { get; set; }

    public string PasswordHash { get; set; }

    public int RoleId { get; set; }
    
    [ForeignKey(nameof(RoleId))]
    public RoleEntity Role { get; set; }

    public bool IsActive { get; set; }
}