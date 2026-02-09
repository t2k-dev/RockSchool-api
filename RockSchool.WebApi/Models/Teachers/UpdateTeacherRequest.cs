using System;

namespace RockSchool.WebApi.Models.Teachers
{
    public class UpdateTeacherRequest
    {
        public TeacherInfo Teacher { get; set; }
        public bool DisciplinesChanged { get; set; }
    }
}
