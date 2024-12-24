using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Entities
{
    public class Discipline
    {
        public int Id { get; set; }
        public string DisciplineName { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public bool IsActive { get; set; }
    }
}
