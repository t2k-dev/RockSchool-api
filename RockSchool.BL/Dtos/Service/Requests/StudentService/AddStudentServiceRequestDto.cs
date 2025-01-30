namespace RockSchool.BL.Dtos.Service.Requests.StudentService;

public class AddStudentServiceRequestDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime BirthDate { get; set; }
    public required short Sex { get; set; }
    public required long Phone { get; set; }
    public required int UserId { get; set; }
}