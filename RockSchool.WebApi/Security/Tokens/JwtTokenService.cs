using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using RockSchool.Domain.Entities;
using RockSchool.WebApi.Models.Account;
using RockSchool.WebApi.Options;

namespace RockSchool.WebApi.Security.Tokens;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public AuthResponse CreateToken(User user, string roleName)
    {
        var expiresAtUtc = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationInMinutes);
        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: BuildClaims(user, roleName),
            expires: expiresAtUtc,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)),
                SecurityAlgorithms.HmacSha256));

        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return new AuthResponse
        {
            UserId = user.Id,
            Login = user.Login,
            RoleId = user.RoleId,
            Role = roleName,
            Token = token,
            ExpiresAtUtc = expiresAtUtc
        };
    }

    private static IEnumerable<Claim> BuildClaims(User user, string roleName)
    {
        yield return new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString());
        yield return new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
        yield return new Claim(ClaimTypes.Name, user.Login);
        yield return new Claim(ClaimTypes.Email, user.Email ?? user.Login);
        yield return new Claim(AuthClaimTypes.RoleId, user.RoleId.ToString(CultureInfo.InvariantCulture));

        if (!string.IsNullOrWhiteSpace(roleName))
            yield return new Claim(ClaimTypes.Role, roleName);
    }
}
