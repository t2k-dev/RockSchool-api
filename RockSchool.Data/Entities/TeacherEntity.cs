using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities;

public class TeacherEntity
{
    [Key] public Guid TeacherId { get; set; }

    [Required] [MaxLength(100)] public string FirstName { get; set; }

    [Required] [MaxLength(100)] public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public int Sex { get; set; }

    public long Phone { get; set; }

    public virtual UserEntity? User { get; set; }

    public int BranchId { get; set; }
    
    public virtual BranchEntity? Branch { get; set; }

    public int AgeLimit { get; set; }

    public bool AllowGroupLessons { get; set; }

    public bool AllowBands { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<DisciplineEntity> Disciplines { get; set; }

    public virtual ICollection<WorkingPeriodEntity> WorkingPeriods { get; set; }

    public virtual ICollection<ScheduledWorkingPeriodEntity> ScheduledWorkingPeriods { get; set; }

    public virtual ICollection<BandEntity>? Bands { get; set; }
}