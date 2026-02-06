using RockSchool.Domain.Teachers;

namespace RockSchool.Domain.Entities;

public class TeacherDiscipline
{
    public Guid TeacherId { get; private set; }
    public int DisciplineId { get; private set; }
    public Teacher Teacher { get; private set; }
    public Discipline Discipline { get; private set; }

    private TeacherDiscipline() { }

    public static TeacherDiscipline Create(Guid teacherId, int disciplineId)
    {
        if (teacherId == Guid.Empty)
            throw new ArgumentException("Teacher ID is required", nameof(teacherId));

        if (disciplineId <= 0)
            throw new ArgumentException("Discipline ID is required", nameof(disciplineId));

        return new TeacherDiscipline
        {
            TeacherId = teacherId,
            DisciplineId = disciplineId
        };
    }
}
