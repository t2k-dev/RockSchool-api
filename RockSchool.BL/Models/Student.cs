using RockSchool.Data.Entities;

namespace RockSchool.BL.Models;

public class Student
{
    public Guid StudentId { get; set; }
    public Guid UserId { get; set; }
    public virtual UserEntity? User { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public short Sex { get; set; }
    public DateTime BirthDate { get; set; }
    public long Phone { get; set; }
    public int? Level { get; set; }
    public virtual Branch? Branch { get; set; }
    public int? BranchId { get; set; }
}