using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Models
{
    public class DisciplineDto
    {
        public int Id { get; set; }

        public string DisciplineName { get; set; }

        public bool IsActive { get; set; }
    }
}
