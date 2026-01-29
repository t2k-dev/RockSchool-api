using RockSchool.Data.Enums;

namespace RockSchool.BL.Models;

public class BandStudent
{
    public Guid BandStudentId { get; set; }
    public Guid BandId { get; set; }
    public virtual Band? Band { get; set; }
    public Guid StudentId { get; set; }
    public virtual Student? Student { get; set; }
    public BandRoleId BandRoleId { get; set; }
}