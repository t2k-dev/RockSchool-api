namespace RockSchool.Data.Entities;

public class StudentEntity
{
    public int StudentId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public DateTime BirthDate { get; set; }
    public long Phone { get; set; }
    public short Sex { get; set; }
    public int? UserId { get; set; }
    public UserEntity UserEntity { get; set; }
}