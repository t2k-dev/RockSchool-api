namespace RockSchool.WebApi.Models.Account;

public class CurrentUserResponse
{
    public Guid UserId { get; set; }
    public string Login { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public string Role { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
