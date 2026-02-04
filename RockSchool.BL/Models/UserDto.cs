
using RockSchool.Domain.Entities;

namespace RockSchool.BL.Models;

public class UserDto
{
    public Guid UserId { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public bool IsActive { get; set; }
}