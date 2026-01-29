using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RockSchool.Data.Enums;

namespace RockSchool.Data.Entities;

public class StudentEntity
{
    [Key]
    public Guid StudentId { get; set; }
    public virtual UserEntity? User { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public short Sex { get; set; }
    public DateTime BirthDate { get; set; }
    public long Phone { get; set; }
    public int? Level { get; set; }
    public virtual BranchEntity? Branch { get; set; }
    public bool IsWaiting { get; set; }
    public virtual ICollection<BandStudentEntity>? BandStudents { get; set; }
}