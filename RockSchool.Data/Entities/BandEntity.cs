using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities;

public class BandEntity
{
    [Key]
    public Guid BandId { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    public Guid TeacherId { get; set; }
    
    [ForeignKey(nameof(TeacherId))]
    public virtual TeacherEntity? Teacher { get; set; }
    
    public int Status { get; set; }
    
    public virtual ICollection<BandStudentEntity>? BandStudents { get; set; }
    
    public virtual ICollection<ScheduleEntity>? Schedules { get; set; }
}