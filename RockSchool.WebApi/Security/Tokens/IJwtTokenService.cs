using RockSchool.Domain.Entities;
using RockSchool.WebApi.Models.Account;

namespace RockSchool.WebApi.Security.Tokens;

public interface IJwtTokenService
{
    AuthResponse CreateToken(User user, string roleName);
}
