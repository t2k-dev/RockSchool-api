using System.ComponentModel.DataAnnotations;

namespace RockSchool.Data.Entities;

public class RoomDisciplineEntity
{
    [Key]
    public Guid RoomDisciplineId { get; set; }

    public virtual DisciplineEntity Discipline { get; set; }
    public virtual RoomEntity Room { get; set; }
}