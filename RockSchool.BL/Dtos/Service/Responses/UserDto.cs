using RockSchool.Data.Entities;

namespace RockSchool.BL.Dtos.Service.Responses;

public class UserDto
{
    public int Id { get; set; }

    public string Login { get; set; }

    public string PasswordHash { get; set; }

    public int RoleId { get; set; }

    public RoleEntity RoleEntity { get; set; }
}