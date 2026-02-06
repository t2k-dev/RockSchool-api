namespace RockSchool.BL.Teachers.AddTeacher
{
    public interface IAddTeacherService
    {
        public Task<Guid> Handle(TeacherDto request);
    }
}
