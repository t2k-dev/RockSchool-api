namespace RockSchool.Domain.Entities;

public class Room
{
    public int RoomId { get; private set; }
    public string Name { get; private set; }
    public int BranchId { get; private set; }
    public Branch Branch { get; private set; }
    public bool SupportsRent { get; private set; }
    public bool SupportsRehearsal { get; private set; }
    public bool IsActive { get; private set; }
    public int? Status { get; private set; }

    private readonly List<RoomDiscipline> _roomDisciplines = new();
    private readonly List<Schedule> _schedules = new();

    public IReadOnlyCollection<RoomDiscipline> RoomDisciplines => _roomDisciplines.AsReadOnly();
    public IReadOnlyCollection<Schedule> Schedules => _schedules.AsReadOnly();

    private Room() { }

    public static Room Create(string name, int branchId, bool supportsRent = false, bool supportsRehearsal = false)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Room name is required", nameof(name));

        if (branchId <= 0)
            throw new ArgumentException("Branch ID must be positive", nameof(branchId));

        return new Room
        {
            Name = name,
            BranchId = branchId,
            SupportsRent = supportsRent,
            SupportsRehearsal = supportsRehearsal,
            IsActive = true,
            Status = 0
        };
    }

    public void UpdateInfo(string name, int branchId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Room name is required", nameof(name));

        if (branchId <= 0)
            throw new ArgumentException("Branch ID must be positive", nameof(branchId));

        Name = name;
        BranchId = branchId;
    }

    public void SetRentSupport(bool supportsRent)
    {
        SupportsRent = supportsRent;
    }

    public void SetRehearsalSupport(bool supportsRehearsal)
    {
        SupportsRehearsal = supportsRehearsal;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void UpdateStatus(int status)
    {
        Status = status;
    }

    public void AddDiscipline(Discipline discipline)
    {
        if (discipline == null)
            throw new ArgumentNullException(nameof(discipline));

        if (_roomDisciplines.Any(rd => rd.DisciplineId == discipline.DisciplineId))
            throw new InvalidOperationException("Discipline is already assigned to this room");

        var roomDiscipline = RoomDiscipline.Create(RoomId, discipline.DisciplineId);
        _roomDisciplines.Add(roomDiscipline);
    }

    public void RemoveDiscipline(int disciplineId)
    {
        var roomDiscipline = _roomDisciplines.FirstOrDefault(rd => rd.DisciplineId == disciplineId);
        if (roomDiscipline != null)
        {
            _roomDisciplines.Remove(roomDiscipline);
        }
    }
}
