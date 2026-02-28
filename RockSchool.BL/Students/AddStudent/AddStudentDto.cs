namespace RockSchool.BL.Students.AddStudent;

public class AddStudentDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public short Sex { get; set; }
    public long Phone { get; set; }
    public int? Level { get; set; }
    public int BranchId { get; set; }
}
