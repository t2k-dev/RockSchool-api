using RockSchool.Data.Entities;
using RockSchool.Data.Enums;

namespace RockSchool.BL.Dtos;

public class StudentDto
{
    public Guid StudentId { get; set; }
    
    public virtual UserEntity? User { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public short Sex { get; set; }
    public DateTime BirthDate { get; set; }
    public long Phone { get; set; }
    public int? Level { get; set; }
    public virtual BranchDto? Branch { get; set; }
    public int? BranchId { get; set; }
}