namespace RockSchool.BL.Dtos;

public class AvailableTeacherDto
{
    public Guid TeacherId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int WorkLoad { get; set; }


}