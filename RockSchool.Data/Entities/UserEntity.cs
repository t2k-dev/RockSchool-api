using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RockSchool.Data.Entities;

public class UserEntity : IdentityUser<Guid>
{
    public Guid UserId { get; set; }

    public string Login { get; set; }

    public int RoleId { get; set; }

    public RoleEntity Role { get; set; }

    public bool IsActive { get; set; }
}