namespace RockSchool.BL.Dtos.Service.Requests.StudentService;

public class UpdateStudentServiceRequestDto
{
    public int StudentId { get; set; }
    public string Login { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public short Sex { get; set; }
    public long Phone { get; set; }
}