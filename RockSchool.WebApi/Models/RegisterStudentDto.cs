using System;

namespace RockSchool.WebApi.Models
{
    public class RegisterStudentDto
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public short Sex { get; set; }
        public long Phone { get; set; }
    }
}
