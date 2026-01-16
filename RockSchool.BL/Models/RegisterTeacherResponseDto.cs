namespace RockSchool.BL.Models;

public class RegisterTeacherResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Guid? TeacherId { get; set; }
    public Guid? UserId { get; set; }
}
