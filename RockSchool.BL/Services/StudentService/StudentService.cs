using AutoMapper;
using RockSchool.BL.Dtos.Service.Requests.StudentService;
using RockSchool.BL.Dtos.Service.Responses;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly StudentRepository _studentRepository;
        protected readonly IMapper _mapper;

        public StudentService(StudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task AddStudentAsync(
            AddStudentServiceRequestDto addStudentServiceRequestDto)
        {
            var studentEntity = _mapper.Map<StudentEntity>(addStudentServiceRequestDto);

            await _studentRepository.AddAsync(studentEntity);

            var savedStudent = await _studentRepository.GetByIdAsync(studentEntity.StudentId);

            if (savedStudent == null)
                throw new InvalidOperationException("Failed to add studentEntity.");
        }

        public async Task UpdateStudentAsync(
            UpdateStudentServiceRequestDto updateStudentServiceRequestDto)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(updateStudentServiceRequestDto.StudentId);

            if (existingStudent == null)
                throw new NullReferenceException("StudentEntity not found.");

            ModifyStudentAttributes(updateStudentServiceRequestDto, existingStudent);

            await _studentRepository.UpdateAsync(existingStudent);
        }

        private static void ModifyStudentAttributes(UpdateStudentServiceRequestDto updateStudentServiceRequestDto,
            StudentEntity existingStudentEntity)
        {
            if (!string.IsNullOrEmpty(updateStudentServiceRequestDto.FirstName))
                existingStudentEntity.FirstName = updateStudentServiceRequestDto.FirstName;

            if (!string.IsNullOrEmpty(updateStudentServiceRequestDto.LastName))
                existingStudentEntity.LastName = updateStudentServiceRequestDto.LastName;

            if (updateStudentServiceRequestDto.BirthDate != default)
                existingStudentEntity.BirthDate = updateStudentServiceRequestDto.BirthDate;

            if (updateStudentServiceRequestDto.Sex != default)
                existingStudentEntity.Sex = updateStudentServiceRequestDto.Sex;

            if (updateStudentServiceRequestDto.Phone != default)
                existingStudentEntity.Phone = updateStudentServiceRequestDto.Phone;

            // if (!string.IsNullOrEmpty(updateStudentServiceRequestDto.Login))
            //     existingStudentEntity.Login = updateStudentServiceRequestDto.Login;
        }

        public async Task<StudentDto[]?> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            var studentsDtos = _mapper.Map<StudentDto[]>(students);

            return studentsDtos;
        }

        public async Task DeleteStudentAsync(int id)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(id);

            if (existingStudent == null)
                throw new InvalidOperationException("StudentEntity not found.");

            await _studentRepository.DeleteAsync(existingStudent);
        }
    }
}