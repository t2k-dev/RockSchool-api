namespace RockSchool.BL.Models;

public class RegisterStudentResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Guid? StudentId { get; set; }
    public Guid? UserId { get; set; }
}
