using System.ComponentModel.DataAnnotations;

namespace RockSchool.Data.Entities;

public class DisciplineEntity
{
    [Key] 
    public int DisciplineId { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<TeacherEntity> Teachers { get; set; }
}