using RockSchool.Data.Entities;

namespace RockSchool.BL.Dtos.Service.Requests.TeacherService;

public class AddTeacherServiceRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public long Phone { get; set; }
    public int[] Disciplines { get; set; }
    public Guid UserId { get; set; }
}