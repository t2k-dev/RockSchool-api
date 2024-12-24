using Microsoft.AspNetCore.Identity;
using RockSchool.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RockSchool.WebApi.Entities;

namespace RockSchool.WebApi.Services
{
    public class UserService
    {
        protected readonly RockSchoolContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(RockSchoolContext rockSchoolContext, IPasswordHasher<User> passwordHasher)
        {
            _context = rockSchoolContext;
            _passwordHasher = passwordHasher;
        }

        public int AddUser(string login, int roleId)
        {
            var newUser = new User()
            {
                Login = login,
                RoleId = roleId
            };

            var passwordHash = _passwordHasher.HashPassword(newUser, "123456");
            newUser.PasswordHash = passwordHash;

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser.Id;
        }
    }
}
