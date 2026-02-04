namespace RockSchool.Domain.Entities;

public class Branch
{
    public int BranchId { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }

    private readonly List<Room> _rooms = new();
    private readonly List<Teacher> _teachers = new();

    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();
    public IReadOnlyCollection<Teacher> Teachers => _teachers.AsReadOnly();

    private Branch() { }

    public static Branch Create(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Branch name is required", nameof(name));

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Branch address is required", nameof(address));

        return new Branch
        {
            Name = name,
            Address = address
        };
    }

    public void UpdateInfo(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Branch name is required", nameof(name));

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Branch address is required", nameof(address));

        Name = name;
        Address = address;
    }
}
