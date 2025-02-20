using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities;

public class TeacherEntity
{
    [Key] public Guid TeacherId { get; set; }

    [Required] [MaxLength(100)] public string FirstName { get; set; }

    [Required] [MaxLength(100)] public string LastName { get; set; }

    [MaxLength(100)] public string MiddleName { get; set; }

    public DateTime BirthDate { get; set; }

    public int Sex { get; set; }

    public long Phone { get; set; }

    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))] public virtual UserEntity User { get; set; }

    public int BranchId { get; set; }
    [ForeignKey(nameof(BranchId))] public virtual BranchEntity Branch { get; set; }

    public int AgeLimit { get; set; }
    public bool AllowGroupLessons { get; set; }
    public virtual ICollection<DisciplineEntity> Disciplines { get; set; }
}