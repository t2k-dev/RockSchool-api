namespace RockSchool.Data.Entities;

public class UserEntity
{
    public int Id { get; set; }

    public string Login { get; set; }

    public string PasswordHash { get; set; }

    public int RoleId { get; set; }

    public RoleEntity RoleEntity { get; set; }
}