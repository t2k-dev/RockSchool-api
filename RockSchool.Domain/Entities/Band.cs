using RockSchool.Domain.Enums;
using RockSchool.Domain.Students;
using RockSchool.Domain.Teachers;

namespace RockSchool.Domain.Entities;

public class Band
{
    public Guid BandId { get; private set; }
    public string Name { get; private set; }
    public Guid TeacherId { get; private set; }
    public Teacher Teacher { get; private set; }
    public int Status { get; private set; }
    public bool IsActive { get; private set; }

    public Guid? ScheduleId { get; private set; }
    public Schedule? Schedule { get; private set; }

    private readonly List<BandMember> _bandMembers = new();

    public IReadOnlyCollection<BandMember> BandMembers => _bandMembers.AsReadOnly();

    private Band() { }

    public static Band Create(string name, Guid teacherId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Band name is required", nameof(name));

        if (teacherId == Guid.Empty)
            throw new ArgumentException("Teacher ID is required", nameof(teacherId));

        return new Band
        {
            BandId = Guid.NewGuid(),
            Name = name,
            TeacherId = teacherId,
            Status = 1, // Active
            IsActive = true
        };
    }

    public void AssignSchedule(Guid scheduleId)
    {
        ScheduleId = scheduleId;
    }

    public void ChangeTeacher(Guid teacherId)
    {
        if (teacherId == Guid.Empty)
            throw new ArgumentException("Teacher ID is required", nameof(teacherId));

        TeacherId = teacherId;
    }

    public void SetStatus(int status)
    {
        Status = status;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void AddMember(Guid studentId, BandRoleId? bandRoleId)
    {
        if (_bandMembers.Any(bm => bm.StudentId == studentId))
            throw new InvalidOperationException("Student is already a member of this band");

        var bandMember = BandMember.Create(BandId, studentId, bandRoleId);
        _bandMembers.Add(bandMember);
    }

    public void RemoveMember(Guid studentId)
    {
        var bandMember = _bandMembers.FirstOrDefault(bm => bm.StudentId == studentId);
        if (bandMember != null)
        {
            _bandMembers.Remove(bandMember);
        }
    }
}
