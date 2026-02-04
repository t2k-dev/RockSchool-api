using RockSchool.Domain.Enums;

namespace RockSchool.Domain.Entities;

public class BandStudent
{
    public Guid BandStudentId { get; private set; }
    public Guid BandId { get; private set; }
    public Band Band { get; private set; }
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; }
    public BandRoleId BandRoleId { get; private set; }

    private BandStudent() { }

    public static BandStudent Create(Guid bandId, Guid studentId, int bandRoleId)
    {
        if (bandId == Guid.Empty)
            throw new ArgumentException("Band ID is required", nameof(bandId));

        if (studentId == Guid.Empty)
            throw new ArgumentException("Student ID is required", nameof(studentId));

        return new BandStudent
        {
            BandStudentId = Guid.NewGuid(),
            BandId = bandId,
            StudentId = studentId,
            BandRoleId = (BandRoleId)bandRoleId
        };
    }

    public void ChangeRole(BandRoleId newRole)
    {
        BandRoleId = newRole;
    }
}
