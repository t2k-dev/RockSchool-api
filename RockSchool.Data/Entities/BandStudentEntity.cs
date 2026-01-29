using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RockSchool.Data.Enums;

namespace RockSchool.Data.Entities;

public class BandStudentEntity
{
    [Key]
    public Guid BandStudentId { get; set; }
    
    public Guid BandId { get; set; }
    
    [ForeignKey(nameof(BandId))]
    public virtual BandEntity? Band { get; set; }
    
    public Guid StudentId { get; set; }
    
    [ForeignKey(nameof(StudentId))]
    public virtual StudentEntity? Student { get; set; }
    
    public BandRoleId BandRoleId { get; set; }
}