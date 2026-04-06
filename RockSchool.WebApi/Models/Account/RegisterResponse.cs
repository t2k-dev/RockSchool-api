namespace RockSchool.WebApi.Models.Account;

public class RegisterResponse
{
    public string Login { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
