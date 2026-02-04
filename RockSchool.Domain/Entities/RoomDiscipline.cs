namespace RockSchool.Domain.Entities;

public class RoomDiscipline
{
    public int RoomId { get; private set; }
    public int DisciplineId { get; private set; }
    public Room Room { get; private set; }
    public Discipline Discipline { get; private set; }

    private RoomDiscipline() { }

    public static RoomDiscipline Create(int roomId, int disciplineId)
    {
        if (roomId <= 0)
            throw new ArgumentException("Room ID is required", nameof(roomId));

        if (disciplineId <= 0)
            throw new ArgumentException("Discipline ID is required", nameof(disciplineId));

        return new RoomDiscipline
        {
            RoomId = roomId,
            DisciplineId = disciplineId
        };
    }
}
