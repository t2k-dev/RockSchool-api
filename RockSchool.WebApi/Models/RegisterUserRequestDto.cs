namespace RockSchool.WebApi.Models;

public class RegisterUserRequestDto
{
    public string Login { get; set; } = string.Empty;
    public int RoleId { get; set; } = 1;
}
