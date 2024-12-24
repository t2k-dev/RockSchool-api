using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RockSchool.WebApi.Entities;

namespace RockSchool.WebApi.Services
{
    public class BaseService
    {
        protected readonly RockSchoolContext _context;

        public BaseService(RockSchoolContext context)
        {
            _context = context;
        }
    }
}
