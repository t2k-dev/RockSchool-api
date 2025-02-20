using RockSchool.Data.Entities;

namespace RockSchool.BL.Dtos.Service.Responses;

public class TeacherDto
{
    public Guid TeacherId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public long Phone { get; set; }
    public ICollection<DisciplineEntity> Disciplines { get; set; }
    public Guid? UserId { get; set; }
    public UserEntity UserEntity { get; set; }
}