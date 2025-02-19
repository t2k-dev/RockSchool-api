using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities;

public class StudentEntity
{
    [Key]
    public int StudentId { get; set; }
    
    public virtual UserEntity? User { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public short Sex { get; set; }
    public DateTime BirthDate { get; set; }
    public long Phone { get; set; }
    public string Level { get; set; }
}