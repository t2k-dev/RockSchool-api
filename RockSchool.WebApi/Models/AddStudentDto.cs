using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Models
{
    public class AddStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public short Sex { get; set; }
        public long Phone { get; set; }
        public int UserId { get; set; }
    }
}
