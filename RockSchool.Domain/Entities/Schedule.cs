namespace RockSchool.Domain.Entities;

public class Schedule
{
    public Guid ScheduleId { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; set; }

    private readonly List<ScheduleSlot> _scheduleSlots = new();
    public IReadOnlyCollection<ScheduleSlot> ScheduleSlots => _scheduleSlots.AsReadOnly();

    // Constructor for EF
    private Schedule() { }

    // Factory method for creation
    public static Schedule Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Schedule name cannot be empty", nameof(name));

        return new Schedule
        {
            ScheduleId = Guid.NewGuid(),
            Name = name,
            IsActive = true
        };
    }

    public void AddScheduleSlot(ScheduleSlot slot)
    {
        if (slot == null)
            throw new ArgumentNullException(nameof(slot));

        if (slot.ScheduleId != ScheduleId)
            throw new InvalidOperationException("Schedule slot does not belong to this schedule");

        _scheduleSlots.Add(slot);
    }

    public void RemoveScheduleSlot(Guid slotId)
    {
        var slot = _scheduleSlots.FirstOrDefault(s => s.ScheduleSlotId == slotId);
        if (slot != null)
            _scheduleSlots.Remove(slot);
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Schedule name cannot be empty", nameof(name));

        Name = name;
    }
}
