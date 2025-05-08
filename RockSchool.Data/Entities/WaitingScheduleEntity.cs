using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RockSchool.Data.Entities
{
    public class WaitingScheduleEntity
    {
        [Key]
        public Guid ScheduleId { get; set; }

        public Guid StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public StudentEntity Student { get; set; }

        public int DisciplineId { get; set; }
        [ForeignKey(nameof(DisciplineId))]
        public virtual DisciplineEntity Discipline { get; set; }

        public Guid TeacherId { get; set; }

        [ForeignKey(nameof(TeacherId))]
        public virtual TeacherEntity? Teacher { get; set; }

        public DateTime CreatedOn { get; set; }

        public int WeekDay { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
