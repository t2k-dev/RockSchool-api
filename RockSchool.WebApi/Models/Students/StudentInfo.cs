using RockSchool.Domain.Entities;
using System;

namespace RockSchool.WebApi.Models.Students
{
    public class StudentInfo
    {
        public Guid StudentId { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public int Age { get; set; }

        public long? Phone { get; set; }

        public int Level { get; set; }

        public int BranchId { get; set; }
    }
}
