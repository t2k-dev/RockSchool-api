using RockSchool.Domain.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockSchool.BL.Teachers.AvailableTeachers
{
    public interface IAvailableTeachersService
    {
        Task<AvailableTeachersDto[]> GetAvailableTeachersAsync(int disciplineId, int branchId, int studentAge);
    }
}
