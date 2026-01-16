namespace RockSchool.BL.Models;

public class RegisterUserResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Guid? UserId { get; set; }
}
