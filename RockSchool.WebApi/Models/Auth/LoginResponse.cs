namespace RockSchool.WebApi.Models.Auth;

public class LoginResponse
{
    public string Token { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
}
