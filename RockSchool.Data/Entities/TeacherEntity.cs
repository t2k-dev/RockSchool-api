using System.ComponentModel.DataAnnotations.Schema;

namespace RockSchool.Data.Entities
{
    public class TeacherEntity
    {
        public int TeacherId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public long Phone { get; set; }
        public ICollection<DisciplineEntity> Disciplines { get; set; }
        public int? UserId { get; set; }
        public UserEntity UserEntity { get; set; }
        [Column(TypeName ="jsonb")]
        public WorkingHoursEntity WorkingHoursEntity { get; set; }
    }
}
