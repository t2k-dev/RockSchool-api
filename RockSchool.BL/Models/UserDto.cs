using RockSchool.Data.Entities;

namespace RockSchool.BL.Models;

public class UserDto
{
    public Guid UserId { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; }
    public bool IsActive { get; set; }
}