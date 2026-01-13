using RockSchool.Data.Entities;

namespace RockSchool.BL.Services.AuthorizationService;

public interface IJwtTokenService
{
    string GenerateJwtToken(UserEntity user, string roleName);
}