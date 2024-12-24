using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
