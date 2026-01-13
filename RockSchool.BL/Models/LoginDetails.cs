namespace RockSchool.BL.Models;

public class LoginDetails
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
}