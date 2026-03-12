using RockSchool.Domain.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Teachers;

namespace RockSchool.BL.Teachers.AddTeacher
{
    public class AddTeacherService(ITeacherRepository teacherRepository, IDisciplineRepository disciplineRepository, IUnitOfWork unitOfWork): IAddTeacherService
    {
        public async Task<Guid> Handle(TeacherDto request)
        {
            var teacher = Teacher.Create(
                request.FirstName, 
                request.LastName, 
                request.BirthDate, 
                request.Sex,
                request.Phone, 
                request.BranchId, 
                request.AgeLimit,
                request.AllowGroupLessons,
                request.AllowBands
                );

            var disciplines = await disciplineRepository.GetByIdsAsync(request.DisciplineIds);
            teacher.UpdateDisciplines(disciplines);

            await teacherRepository.AddAsync(teacher);
            await unitOfWork.SaveChangesAsync();

            return teacher.TeacherId;
        }
    }
}
