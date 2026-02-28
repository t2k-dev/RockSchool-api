namespace RockSchool.BL.Teachers.AvailableTeachers
{
    public interface IAvailableTeachersService
    {
        Task<AvailableTeachersDto[]> GetAvailableTeachersAsync(int disciplineId, int branchId, int studentAge);
        Task<AvailableTeachersDto> GetAvailableTeacherAsync(Guid teacherId);
    }
}
