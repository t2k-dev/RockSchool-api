using RockSchool.Domain.Enums;
using RockSchool.Domain.Students;

namespace RockSchool.Domain.Entities;

public class BandMember
{
    public Guid BandMemberId { get; private set; }
    public Guid BandId { get; private set; }
    public Band Band { get; private set; }
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; }
    public BandRoleId BandRoleId { get; private set; }

    private BandMember() { }

    public static BandMember Create(Guid bandId, Guid studentId, BandRoleId bandRoleId)
    {
        if (bandId == Guid.Empty)
            throw new ArgumentException("Band ID is required", nameof(bandId));

        if (studentId == Guid.Empty)
            throw new ArgumentException("Student ID is required", nameof(studentId));

        return new BandMember
        {
            BandMemberId = Guid.NewGuid(),
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
