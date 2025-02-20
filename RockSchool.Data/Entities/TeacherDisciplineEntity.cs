using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities;

public class TeacherDisciplineEntity
{
    [Key]
    public Guid TeacherDisciplineId { get; set; }
    public Guid TeacherId { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public virtual TeacherEntity Teacher { get; set; }
    public int DisciplineId { get; set; }
    [ForeignKey(nameof(DisciplineId))] public virtual DisciplineEntity Discipline { get; set; }
}